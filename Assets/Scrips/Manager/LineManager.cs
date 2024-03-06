using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 线条类型枚举
/// </summary>
public enum LineName
{
    WordsLine,
    CloudLine,
    ChatLine,
    WordsLineWall
}

/// <summary>
/// 线条管理器
/// </summary>
public class LineManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static LineManager instance;
    public static LineManager Instance
    {
        get => instance;
    }

    private SpriteRenderer[] LineRenderer;//获取线条

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        instance = this;
        if (GetComponentsInChildren<SpriteRenderer>() != null)
        {
            LineRenderer = GetComponentsInChildren<SpriteRenderer>();//获取所有线条并隐藏
        }
        foreach (var Line in LineRenderer)
        {
            ChangeSprite(Line, false);
        }
    }

    /// <summary>
    /// 初始化线条的方法(隐藏加消除碰撞箱)
    /// </summary>
    /// <param name="Line"></param>
    /// <param name="IsShow"></param>
    private void ChangeSprite(SpriteRenderer Line,bool IsShow)
    {
        if (Line.enabled != IsShow)
        {
            Line.enabled = IsShow;
            Line.gameObject.transform.GetComponent<BoxCollider2D>().enabled = IsShow;
        }
    }

    /// <summary>
    /// 显示指定类型的线条
    /// </summary>
    /// <param name="lineName"></param>指定的类型
    /// <param name="Num"></param>哪个线条
    public void ShowLine(LineName lineName, int Num)
    {
        if (Num >= 10)
        {
            foreach (var Line in LineRenderer)
            {
                if (Line.gameObject.name == lineName.ToString()+"_" + Num)
                {
                    ChangeSprite(Line, true);
                }
            }
        }
        else
        {
            foreach (var Line in LineRenderer)
            {
                if (Line.gameObject.name == lineName.ToString()+"_0" + Num)
                {
                    ChangeSprite(Line, true);
                }
            }
        }
    }
    
    /// <summary>
    /// 隐藏指定类型的线条
    /// </summary>
    /// <param name="lineName"></param>线条的类型
    /// <param name="Num"></param>哪个线条
    public void HideLine(LineName lineName, int Num)
    {
        if (Num >= 10)
        {
            foreach (var Line in LineRenderer)
            {
                if (Line.gameObject.name == lineName.ToString()+"_" + Num)
                {
                    ChangeSprite(Line, false);
                }
            }
        }
        else
        {
            foreach (var Line in LineRenderer)
            {
                if (Line.gameObject.name == lineName.ToString()+"_0" + Num)
                {
                    ChangeSprite(Line, false);
                }
            }
        }
    }

}
