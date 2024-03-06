using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 玩家脚本
/// </summary>
public class Player : MonoBehaviour
{
    public LayerMask ground;//地面层级,表示落地才能跳
    public bool IsMove = true;//是否处于移动状态有用的有时候要取消玩家的移动 强制位移
    public bool IsCross = true;//判断是否能过穿透线条

    public new Rigidbody2D rigidbody;//获取刚体组件进行移动
    private Animator animator;//获取的玩家的动画状态机
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsule;
    private int speed = 1;//设置玩家移动的速度
    private int HorMoveDir;//玩家移动情况(左还是右)
    private int HorDir;//玩家的面朝方向
    private float jumpforce = 2f;//设置玩家的跳跃强度
    private int NowNum = 1;//玩家当前书写笔画的笔序
    private bool isOnGround = false; //判断当前是否在地面
    private bool IsJump = false;//是否处于长跳跃
    private bool IsPick = false;//是否可以抓取(节约性能)
    private bool IspickOn = false;//是否已经抓取
    private Transform TextBox;//要抓取的目标方格
    private PickupBox pickupBox;//要抓取的目标方格的脚本

    //这一段是用来实现长跳跃的
    public float jumpholdtime = 0.05f;//跳跃时间
    public  float jumpholdforce = 0.05f;//跳跃的力
    private float jumptime;


    private void Awake()
    {
        //获取最基本的组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (IsMove)//是否处于可运动状态
        {
            Move();
        }

        if (IsPick) //是否处于可拾取状态
        {
            Pick();
        }

        if (IsCross)//是否处于可穿线状态
        {
            CrossLine();
        }
    }
    /// <summary>
    /// 玩家运动脚本
    /// </summary>
    private void Move()
    {
        CheckVerMoveDir();
        CheckHorMoveDir(ref HorMoveDir);
        AnimatorChanger(HorMoveDir);
        rigidbody.velocity = new Vector2(HorMoveDir * speed, rigidbody.velocity.y);
    }
    
    /// <summary>
    /// 水平移动方向
    /// </summary>
    /// <param name="moveDir">返回的向量值</param>
    private void CheckHorMoveDir(ref int HormoveDir)
    {
        //当我按下【负】向键位时 这里暂时先放A 移动方向就是【负】向的
        if (Input.GetKeyDown(KeyCode.A))
        {
            HormoveDir = -1;
        }
        //当我按下【正】向键位时 这里暂时先放D 移动方向就是【正】向的
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            HormoveDir = 1;
        }
        //当我抬起【正】向键位时 检查是否按住了负向按键 如果是 就返回【负】方向 如果不是 就停止移动
        else if (Input.GetKeyUp(KeyCode.D)) HormoveDir = Input.GetKey(KeyCode.A) ? -1 : 0;
        //当我抬起【负】向键位时 检查是否按住了正向按键 如果是 就返回【正】方向 如果不是 就停止移动
        else if (Input.GetKeyUp(KeyCode.A)) HormoveDir = Input.GetKey(KeyCode.D) ? 1 : 0;

        if (HormoveDir != 0)
        {
            transform.localScale = new Vector3(HormoveDir, 1, 1);
            HorDir = HormoveDir;
        }
    }

    /// <summary>
    /// 竖直轴移动方向
    /// </summary>
    /// <param name="VermoveDir"></param>
    private void CheckVerMoveDir()
    {
        isOnGround = rigidbody.IsTouchingLayers(ground);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.Jump);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("isJump");//修改当前动画状态机为跳跃
            }

            if (IsJump)
            {
                jumptime = Time.time + jumpholdtime;
            }
            rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);//调整施加力的方式，瞬间冲力
        }

        if (IsJump)//长跳跃
        {
            if (!isOnGround && Input.GetKey(KeyCode.Space) && Time.time < jumptime)
            {
                rigidbody.AddForce(new Vector2(0, jumpholdforce), ForceMode2D.Impulse);
            }
        }
        
    }

    /// <summary>
    /// 按照当前的运动情况修改动画状态机
    /// </summary>
    /// <param name="HorMoveDir"></param>
    /// 
    public void AnimatorChanger(int HorMoveDir)
    {
        if (HorMoveDir != 0)
        {
            if (!animator.GetBool("isWalk"))
            {
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            if (animator.GetBool("isWalk"))
            {
                animator.SetBool("isWalk", false);
            }
        }
    }

    /// <summary>
    /// 触发检测(抓方块)
    /// </summary>
    /// <param name="coll"></param>
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "TextBox")
        {
            if (!IspickOn)
            {
                if (IsPick == false)
                {
                    IsPick = true;//修改当前为可抓取状态
                }
                TextBox = coll.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "TextBox" && !IspickOn)
        {
            if (IsPick == true)
            {
                IsPick = false;//修改当前为可抓取状态
            }
            TextBox = null;
        }

    }

    /// <summary>
    /// 碰撞触发器(撞击方块)
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Stroke>() != null)
        {
            Stroke stroke = coll.gameObject.GetComponent<Stroke>(); 
            string isNow = stroke.WriteOut(NowNum);//撞击后获取当前书写笔画是否正确

            if (isNow == "isOk")//正确进入下一个笔画
            {
                NowNum++;
                AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.CorrectClip);
            }
            else if (isNow == "isLast")//结束初始化笔画序号
            {
                NowNum = 1;
                AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.CorrectClip);
            }
            else if (isNow == "isNo")//出现错误
            {
                AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ErrorClip);
            }
        }
    }

    /// <summary>
    /// 玩家抓取的脚本
    /// </summary>
    private void Pick()
    {
        if (Input.GetKeyDown(KeyCode.J) && !IspickOn)//按下J且玩家还没有抓取物体
            {
                pickupBox = TextBox.GetComponent<PickupBox>();
                if (pickupBox.Isok == true) return;//判断方块是否已经处于放置成功状态
                TextBox.position = transform.position + new Vector3(0.13f * HorDir, 0f, 0);
                IspickOn = true;//玩家进入抓取状态
                pickupBox.IspickOn = true;//且把目标方格的状态也修改为抓取状态
            }

        if (IspickOn)//当玩家进入抓取状态时时刻刻修改方格的坐标
        {
            TextBox.position = transform.position + new Vector3(0.13f * HorDir, 0f, 0);
        }

        if (Input.GetKeyDown(KeyCode.K) && IspickOn)//当玩家处于抓取状态并且按下了K键
        {
            if (pickupBox.Isflage)//由方格自身判断方格是否处于合适的位置
            {
                IspickOn = false;
                pickupBox.IspickOn = false;
                IsPick = false;
                TextBox = null;
            }
        }

       
    }

    /// <summary>
    /// 跳跃穿过线条
    /// </summary>
    private void CrossLine()
    {
        if (rigidbody.velocity.y > 0.1f)
        {
            capsule.enabled = false;
        }
        else
        {
            capsule.enabled = true;
        }
    }

    /// <summary>
    /// 修改玩家本身的是否运动和隐藏与显示
    /// </summary>
    /// <param name="isMove"></param>表示是否运动
    /// <param name="isShow"></param>表示是否显示
    /// <param name="ChangeTime"></param>表示修改显示情况需要的事件
    public void RevampPlayer(bool isMove, bool isShow, bool isGravity = true,float ChangeTime = 0.1f)
    {
        IsMove = isMove;
        HorMoveDir = 0;
        if (isShow)
        {
            spriteRenderer.DOFade(1, ChangeTime);
            
        }
        else
        {
            spriteRenderer.DOFade(0, ChangeTime);
            
        }

        if (isGravity)
        {
            rigidbody.gravityScale = 1;
        }
        else
        {
            rigidbody.gravityScale = 0;
        }
    }
}
