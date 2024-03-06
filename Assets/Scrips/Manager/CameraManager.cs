using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 摄像机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static CameraManager instance;
    public static CameraManager Instance
    {
        get => instance;
    }
    public float CamOffsetMax = 10;
    public float CamOffsetMin = 0;
    private Transform target;//玩家的Transform
    private Transform Tail;//获取在场景变换中使用到的路径
    //private Vector3 baseVector3;//初始位置
    private Vector3 secondVector3;//第二部分初始位置
    public bool isShow;//是否当前是否是跟随摄影状态
    public bool isSecond = false;//是否进入第二部分

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        isSecond = false;
        isShow = false;
        instance = this;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        InitVector3();
       // transform.position = baseVector3;//修改开始位置摄像机的坐标
        target = GameManager.Instance.Player.transform;//获取玩家的Transfrom
    }

    /// <summary>
    /// 时刻判断摄像机状态,并运行
    /// </summary>
    private void Update()
    {
        if (isSecond)
        {
            if (this.transform.position != secondVector3)
            {
                this.transform.DOMove(secondVector3,0.5f);
            }
            return;
        }
        if (!isShow)//当不是处于展示摄影的时候就进行跟随摄影
        {
            Followpattern();
        }
        else
        {
            Showpattern();
        }

    }

    //摄像机限定视角
    private void LateUpdate()
    {

    }

    /// <summary>
    /// 初始化两个部分的摄像机的坐标
    /// </summary>
    private void InitVector3()
    {
        switch (GameManager.Instance.Guanka)//根据关卡标识位修改摄像机的两个位置(开始位置,第二关位置)
        {
            case Guanka.YGYS:
                //baseVector3 = GameManager.Instance.gameConf.YgysVector3;
                secondVector3 = GameManager.Instance.gameConf.YgysSecondCameravVector3;
                break;
            case Guanka.YMZZ:
                //baseVector3 = GameManager.Instance.gameConf.YmzzVector3;
                secondVector3 = GameManager.Instance.gameConf.YmzzSecondCameravVector3;
                break;
            case Guanka.FJQZ:
               // baseVector3 = GameManager.Instance.gameConf.FjqzVector3;
                secondVector3 = GameManager.Instance.gameConf.FjqzSecondCameravVector3;
                break;
            case Guanka.CMLX:
                //baseVector3 = GameManager.Instance.gameConf.CmlxVector3;
                secondVector3 = GameManager.Instance.gameConf.CmlxSecondCameravVector3;
                break;

        }
    }

    /// <summary>
    /// 修改当前的摄像机摄影的状态
    /// </summary>
    /// <param name="isshow"></param>是否为显示摄影
    /// <param name="tail"></param>,显示摄影中使用到的路径
    public void ChangeShow(bool isshow, Transform tail = null)
    {
        isSecond = false;
        this.isShow = isshow;

        if (isshow)
        {
            if (tail == null)
            {
                return;
            }
            Tail = tail;
        }

    }

    /// <summary>
    /// 修改摄像机为第二关的状态
    /// </summary>
    public void ChangSecond()
    {
        if (Camera.main.orthographicSize != 1.6f)
        {
            Camera.main.DOOrthoSize(1.6f, 1);
        }
        isSecond = true;
    }

    /// <summary>
    /// 跟随摄影
    /// </summary>
    private void Followpattern()
    {
        if (Camera.main.orthographicSize != 1.6f)
        {
            Camera.main.DOOrthoSize(1.6f, 1);
        }
        float Posx = target.position.x;
        float PosY = target.position.y;
        this.transform.DOMoveX(Mathf.Clamp(Posx,CamOffsetMin,CamOffsetMax), 0.5f);
        this.transform.DOMoveY(PosY-0.8f,1f);
    }

    /// <summary>
    /// 展示摄影
    /// </summary>
    private void Showpattern()
    {
        if (Camera.main.orthographicSize != 1)
        {
            Camera.main.DOOrthoSize(1f, 1f);
        }
        Vector3 pos = Tail.position + new Vector3(0, 0, -5);
        this.transform.DOMove(pos, 0.5f);
    }


}
