using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//添加碰撞箱和刚体
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

/// <summary>
/// 抓取盒脚本
/// </summary>
public class PickupBox : MonoBehaviour
{
    public string FlagName;//目标放置盒的名字
    public int Strlong;//抓取盒的文本长度
    private Vector3 startVector3;//抓取盒的初始位置
    private bool ispickOn;//是否处于抓取状态
    private bool isflage;//是否到达目标区域
    private bool isok;//是否到达正确的目标区域

    public bool Isflage => isflage;//可供外界获取是否到达可放置位置

    public bool Isok => isok;//可供外界获取是否到达正确放置位置

    public bool IspickOn
    {
        get => ispickOn;//可供外界获取当前抓取盒是否被抓取

        set
        {
            ispickOn = value;
            if (value) return;//如果是修改为抓状态直接退出
            if (isok == true)return;//如果修改为放置状态后判断如果到达正确位置也直接退出
            //只有  修改为放置状态并且未到达正确放置位置
            Instantiate(GameManager.Instance.gameConf.Worrg, transform.position, Quaternion.identity).GetComponent<Worry>().Init((() => transform.position = startVector3));
        }

    }

    private void Awake()
    {
        startVector3 = transform.position;//获取初始位置
        transform.tag = "TextBox";//修改标签
        GetComponent<BoxCollider2D>().size = new Vector2(Strlong * 0.1f, 0.1f);//修改碰撞箱的大小
        GetComponent<BoxCollider2D>().isTrigger = true;//修改为触发器
        GetComponent<Rigidbody2D>().isKinematic = true;//修改为力学
    }

    /// <summary>
    /// 触碰到放置盒的时候
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag ==  "PickFlag")
        {
            isflage = true;//到达可放置位置
            if (other.transform.name == FlagName)
            {
                isok = true;//到达正确的可放置位置
            }
            
        }
    }

    /// <summary>
    /// 离开放置盒事
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "PickFlag")
        {
            isflage = false;//恢复为默认状态
            isok = false;
        }
    }
}
