using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文字显示管理器
/// </summary>
public class TypeManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static TypeManager instance;
    public static TypeManager Instance
    {
        get => instance;
    }

    //文字效果脚本
    private TypeEffect[] type;

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        instance = this;
        type = transform.GetComponentsInChildren<TypeEffect>();//获取所有的文字显示效果

    }

    /// <summary>
    /// 开启文字显示
    /// </summary>
    /// <param name="Num"></param>开启哪个文字
    /// <returns></returns>返回当前文字播放所需要的时间
    public float ShowType(int Num)
    {
        return type[Num-1].StartEffect();
    }

    /// <summary>
    /// 隐藏文字
    /// </summary>
    /// <param name="Num"></param>隐藏哪个文字
    public void  HideType(int Num)
    {
        type[Num-1].gameObject.SetActive(false);
    }


}
