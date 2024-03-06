using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 环境管理器
/// </summary>
public class EnvManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static EnvManager instance;
    public static EnvManager Instance
    {
        get => instance;
    }

    private SpriteRenderer[] Env;//获取所有环境的容器
    private Animator[] animators;//获取第二部分四个汉字的动画状态机
    private EdgeCollider2D board;//第二部分草地的线型碰撞箱

    //提供一个可供外界调用的变量
    public EdgeCollider2D Board => board;
    public Animator[] Animators => animators;
   
    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        instance = this;
        animators = GetComponentsInChildren<Animator>();//获取的动画状态机
        Env = transform.GetComponentsInChildren<SpriteRenderer>();//获取所有景物
        foreach (SpriteRenderer spriteRenderer in Env)
        {
            if (spriteRenderer.gameObject.name == "Board")//将草地的碰撞箱先变成触发器
            {
                board = spriteRenderer.gameObject.GetComponent<EdgeCollider2D>();
                Board.isTrigger = true;
            }
            spriteRenderer.color = Color.clear;//隐藏所有景物
        }
    }

    /// <summary>
    /// 显示环境
    /// </summary>
    /// <param name="Num"></param>第几个环境
    public void ShowEnv(int Num)
    {
        if (Env[Num-1].color.r != 1)
        {
            Env[Num-1].DOColor(Color.white, 1);
        }
    }

    /// <summary>
    /// 隐藏环境
    /// </summary>
    /// <param name="Num"></param>第几个环境
    public void HideEnv(int Num)
    {
        if (Env[Num - 1].color.r != 0)
        {
            Env[Num - 1].DOColor(Color.clear, 1);
        }
    }
}
