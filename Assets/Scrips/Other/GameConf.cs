using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 游戏配置脚本
/// [Header("头标题")]
/// [Tooltip("标签名")] 
/// </summary>
/// 
[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]//文件名和菜单名
public class GameConf : ScriptableObject
{

    [Header("特效")]
    [Tooltip("第一部分移动效果")]
    public GameObject FirstRondTail;
    [Tooltip("第二部分移动效果")]
    public GameObject SecondRondTail;
    [Tooltip("移动结束生成物体")]
    public GameObject Boom;
    [Tooltip("错误生成物体")]
    public GameObject Worrg;
    [Tooltip("正确生成物体")]
    public GameObject Great;
    [Tooltip("声音播放器")]
    public GameObject EFAudio;

    [Header("第一部分移动效果去向的坐标")]
    public Vector3[] YGYSFirstVector3s;
    public Vector3[] YMZZFirstVector3s;
    public Vector3[] FJQZFirstVector3s;
    public Vector3[] CMLXFirstVector3s;

    [Header("第二部分门的坐标")]
    public Vector3 YGYSFSecondDoorVector;
    public Vector3 YMZZSecondDoorVector;
    public Vector3 FJQZSecondDoorVector;
    public Vector3 CMLXSecondDoorVector;

    [Header("笔画方块生成基点")]
    public Vector3 YGYSBh_Base;
    public Vector3 YMZZBh_Base;
    public Vector3 FJQZBh_Base;
    public Vector3 CMLXBh_Base;

    // [Header("摄像机的初始坐标")]
    // public Vector3 YgysVector3;   -2.4 1.8 -5  2.75
    // public Vector3 YmzzVector3;
    // public Vector3 FjqzVector3;
    // public Vector3 CmlxVector3;

    [Header("第二部分摄像机的坐标")]
    public Vector3 YgysSecondCameravVector3;
    public Vector3 YmzzSecondCameravVector3;
    public Vector3 FjqzSecondCameravVector3;
    public Vector3 CmlxSecondCameravVector3;

    [Header("去向第二部分用到的路径的坐")]
    public Transform[] YgysTransform;
    public Transform[] YmzzTransform;
    public Transform[] FjqzTransform;
    public Transform[] CmlxTransform;

    [Header("玩家生成基点")]
    public Vector3 YgysPlayerVector3;
    public Vector3 YmzzPlayerVector3;
    public Vector3 FjqzPlayerVector3;
    public Vector3 CmlxPlayerVector3;

    [Header("坚持不懈")]

    [Tooltip("坚_笔画")]
    public GameObject[] Jian_Bh;
    [Tooltip("持_笔画")]
    public GameObject[] Chi_Bh;
    [Tooltip("不_笔画")]
    public GameObject[] Bu_Bh;
    [Tooltip("懈_笔画")]
    public GameObject[] Xie_Bh;

    [Header("虚心求教")]

    [Tooltip("虚_笔画")]
    public GameObject[] Xu_Bh;
    [Tooltip("心_笔画")]
    public GameObject[] Xing_Bh;
    [Tooltip("求_笔画")]
    public GameObject[] Qiu_Bh;
    [Tooltip("教_笔画")]
    public GameObject[] Jiao_Bh;

    [Header("因材施教")]

    [Tooltip("因_笔画")]
    public GameObject[] Ying_Bh;
    [Tooltip("材_笔画")]
    public GameObject[] Cai_Bh;
    [Tooltip("施_笔画")]
    public GameObject[] Shi_Bh;

    [Header("知错能改")]
    [Tooltip("知_笔画")]
    public GameObject[] Zhi_Bh;
    [Tooltip("错_笔画")]
    public GameObject[] Cuo_Bh;
    [Tooltip("能_笔画")]
    public GameObject[] Neng_Bh;
    [Tooltip("改_笔画")]
    public GameObject[] Gai_Bh;



    [Header("声音")]
    [Tooltip("Book")]
    public AudioClip BookClip;
    [Tooltip("UI1")]
    public AudioClip GameClip;
    [Tooltip("跳跃")] 
    public AudioClip Jump;
    [Tooltip("按钮")]
    public AudioClip ButtonClip;
    [Tooltip("切换")]
    public AudioClip SwitchClip;
    [Tooltip("正确撞击")]
    public AudioClip CorrectClip;
    [Tooltip("错误撞击")]
    public AudioClip ErrorClip;

    [Header("鼠标")]
    [Tooltip("鼠标1")]
    public Texture2D Hand01;
    [Tooltip("错误撞击")]
    public Texture2D Hand02;
}
