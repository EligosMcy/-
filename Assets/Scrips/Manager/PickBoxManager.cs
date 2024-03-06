using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 放置盒,拾取盒的管理器
/// </summary>
public class PickBoxManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static PickBoxManager instance;
    public static PickBoxManager Instance
    {
        get => instance;
    }

    private PickFlag[] pickFlags;//放置盒
    private PickupBox[] pickBoxes;//拾取盒

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        instance = this;
        pickBoxes = transform.GetComponentsInChildren<PickupBox>();//获取所有抓取盒
        pickFlags = transform.GetComponentsInChildren<PickFlag>();//获取所有放置盒
        foreach (PickFlag flag in pickFlags)
        {
            flag.gameObject.SetActive(false);//隐藏所有的放置盒脚本
        }

        foreach (PickupBox Pick in pickBoxes)
        {
            Pick.gameObject.SetActive(false);//隐藏所有的放置盒脚本
        }
    }
    /// <summary>
    /// 显示放置盒
    /// </summary>
    /// <param name="Num"></param>哪个放置盒
    public void ShowFlags(int Num)
    {
        if (pickFlags[Num - 1].gameObject != null)
        {
            pickFlags[Num - 1].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏放置盒
    /// </summary>
    /// <param name="Num"></param>哪个放置盒
    public void HideFlags(int Num)
    {
        if (pickFlags[Num - 1].gameObject != null)
        {
            pickFlags[Num - 1].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 显示抓取盒
    /// </summary>
    /// <param name="Num"></param>哪个抓却盒
    public void ShowPick(int Num)
    {
        if (pickBoxes[Num - 1].gameObject != null)
        {
            pickBoxes[Num - 1].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏抓取框
    /// </summary>
    /// <param name="Num"></param>
    public void HidePick(int Num)
    {
        if (pickBoxes[Num - 1].gameObject != null)
        {
            pickBoxes[Num - 1].gameObject.SetActive(false);
        }
    }

}
