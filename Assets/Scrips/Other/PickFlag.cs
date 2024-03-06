using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//添加碰撞箱
[RequireComponent(typeof(BoxCollider2D))]

/// <summary>
/// 放置盒脚本
/// </summary>
public class PickFlag : MonoBehaviour
{
    public string FlagStr;//放置盒放置后修改的文字
    public int PlanNum;//要开始开启的是哪个阶段
    public int Strlong;//该放置盒的文本长度
    private PickupBox pickupBox;//获取抓取盒的脚本
    private bool IsFirst = true;//是否只放置的一次
    private Trail Trail;//放置盒在被放置后使用到的路径移动脚本

    /// <summary>
    /// 初始化放置盒
    /// </summary>
    private void Awake()
    {
        transform.tag = "PickFlag";//修改标签
        GetComponent<BoxCollider2D>().size = new Vector2(Strlong * 0.1f, 0.1f);//修改放置盒的碰撞箱大小
        GetComponent<BoxCollider2D>().isTrigger = true;//并修改为触发器模式
    }

    /// <summary>
    /// 触碰到抓取盒
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PickupBox>() != null)//当碰到抓取盒的时候
        {
            pickupBox = other.GetComponent<PickupBox>();//获取抓取盒脚本
        }
    }

    /// <summary>
    /// 在抓取盒离开后
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PickupBox>() != null)//当碰到抓取盒的时候
        {
            if (pickupBox != null)
            {
                pickupBox = null;//去除获取到的抓取盒脚本
            }
        }
    }

    /// <summary>
    /// 时时刻刻判断当前是否是放置成功状态
    /// </summary>
    private void Update()
    {
        if (pickupBox != null && IsFirst)//当碰到了抓取盒的时候且是第一次进行放置阶段
        {
            if (pickupBox.Isok && !pickupBox.IspickOn)//当抓取盒处于可放置阶段且不处于抓取状态
            {
                IsFirst = false;//避免重复进行
                Instantiate(GameManager.Instance.gameConf.Great, transform.position, Quaternion.identity).GetComponent<Worry>().Init();
                transform.DOPunchPosition(new Vector3(0, 0.2f, 0), 0.4f, 0, 0.5f);
                transform.DOPunchRotation(new Vector3(0, 0, -0.15f), 0.4f, 0, 0.5f);
                pickupBox.gameObject.transform.DOPunchPosition(new Vector3(0, 0.2f, 0), 0.4f, 0, 0.5f);//自身和放置盒向 上小跳一下
                pickupBox.gameObject.transform.DOPunchRotation(new Vector3(0, 0, -0.15f), 0.4f, 0, 0.5f).OnComplete((() =>
                    {
                        Destroy(pickupBox.gameObject);//跳玩了删除抓取盒
                       
                        ChangeFlag();
                    }));
                

            }
        }
    }

    /// <summary>
    /// 修改放置盒的当前状态和开启下一阶段
    /// </summary>
    private void ChangeFlag()
    {
        switch (GameManager.Instance.Guanka)
        {
            case Guanka.YGYS:
                if (PlanNum == 4)//进行替换文字有差别
                {
                    transform.GetComponent<TextMesh>().text = " ji ben \n箕畚";
                }
                else if (PlanNum != 5)
                {
                    transform.GetComponent<TextMesh>().text = FlagStr;
                }
                GameManager.Instance.Player.RevampPlayer(false, true);//让玩家不能控制
                Trail = Instantiate(GameManager.Instance.gameConf.FirstRondTail, transform.position,
                    Quaternion.identity).GetComponent<Trail>();//实例化路径
                Trail.Init(PlanNum);
                break;
            case Guanka.YMZZ:
                if (PlanNum != 0)
                {
                    transform.GetComponent<TextMesh>().text = FlagStr;
                }
                GameManager.Instance.Player.RevampPlayer(false, true);//让玩家不能控制
                Trail = Instantiate(GameManager.Instance.gameConf.FirstRondTail, transform.position,
                    Quaternion.identity).GetComponent<Trail>();//实例化路径
                Trail.Init(PlanNum);
                break;
            case Guanka.FJQZ:
                if (PlanNum != 0)
                {
                    transform.GetComponent<TextMesh>().text = FlagStr;
                }
                GameManager.Instance.Player.RevampPlayer(false, true);//让玩家不能控制
                Trail = Instantiate(GameManager.Instance.gameConf.FirstRondTail, transform.position,
                    Quaternion.identity).GetComponent<Trail>();//实例化路径
                Trail.Init(PlanNum);
                break;
            case Guanka.CMLX:
                if (PlanNum != 0)
                {
                    transform.GetComponent<TextMesh>().text = FlagStr;
                }
                GameManager.Instance.Player.RevampPlayer(false, true);//让玩家不能控制
                Trail = Instantiate(GameManager.Instance.gameConf.FirstRondTail, transform.position,
                    Quaternion.identity).GetComponent<Trail>();//实例化路径
                Trail.Init( PlanNum);
                break;
        }
    }
}
