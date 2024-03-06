using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

// Xu = 11,Xing = 4,Qiu = 7,Jiao = 11,
// 虚心求教
// Jian = 7,Chi = 9,Bu = 4,Xie = 16
// 坚持不懈
// Zhi = 8,Cuo = 13,Neng = 10,Gai = 7,
// 知错能改
// Ying = 6,Cai = 9,Shi = 9
// 因材施教
public enum Hanzi//笔画标志位
{
    Start,//笔画开始的标志位
    Jian, Chi, Bu, Xie,
    Zhi, Cuo, Neng, Gai,
    Xu, Xing, Qiu,
    Ying, Cai, Shi, Jiao,
}


/// <summary>
/// 笔画管理器
/// </summary>
public class BhManager : MonoBehaviour
{
    //单例模式
    private static BhManager instance;
    public static BhManager Instance
    {
        get => instance;
    }

    private int HanziNum = 1;//书写汉字时的笔画时序
    private Vector3 Base;//生成笔画方块的基准点
    private Vector3 offet = new Vector3(0.5f, 0, 0);//生成笔画方块之间的标志间隔
    private int[] offetList = new int[] {-2, -1, 0, 1, 2};//偏移量数组

    private Stroke stroke;//生成方块的时候修改Stroke脚本的容器
    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {  
        instance = this;//初始化单例模式
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        InitBase();
    }

    private void Update()
    {

    }

    /// <summary>
    /// 根据当前的游戏进度修改笔画方块初始化位置
    /// </summary>
    private void InitBase()
    {
        switch (GameManager.Instance.Guanka)
        {
            case Guanka.YGYS:
                Base = GameManager.Instance.gameConf.YGYSBh_Base;
                break;
            case Guanka.YMZZ:
                Base = GameManager.Instance.gameConf.YMZZBh_Base;
                break;
            case Guanka.FJQZ:
                Base = GameManager.Instance.gameConf.FJQZBh_Base;
                break;
            case Guanka.CMLX:
                Base = GameManager.Instance.gameConf.CMLXBh_Base;
                break;
        }
    }

    /// <summary>
    /// 根据汉字的标识为返回当前汉字的笔画数量
    /// </summary>
    /// <param name="hanzi"></param>汉字标识位
    /// <returns></returns>
    private int HanziToNum(Hanzi hanzi)
    {
        int Num =0 ;
        switch (hanzi)
        {
            case Hanzi.Jian:
                Num = 7;
                break;
            case Hanzi.Chi:
                Num = 9;
                break;
            case Hanzi.Bu:
                Num = 4;
                break;
            case Hanzi.Xie:
                Num = 16;
                break;
            case Hanzi.Xu:
                Num = 11;
                break;
            case Hanzi.Xing:
                Num = 4;
                break;
            case Hanzi.Qiu:
                Num = 7;
                break;
            case Hanzi.Jiao:
                Num = 11;
                break;
            case Hanzi.Zhi:
                Num = 8;
                break;
            case Hanzi.Cuo:
                Num = 13;
                break;
            case Hanzi.Neng:
                Num = 10;
                break;
            case Hanzi.Gai:
                Num = 7;
                break;
            case Hanzi.Ying:
                Num = 6;
                break;
            case Hanzi.Cai:
                Num = 7;
                break;
            case Hanzi.Shi:
                Num = 9;
                break;
        }
        return Num;
    }

    /// <summary>
    /// 重置链表中的数据并打乱链表中的偏移量
    /// </summary>
    /// <param name="arr"></param>
    public static void Sort(ref int[] arr)
    {
        arr = new int[] { -2, -1, 0, 1, 2 };
        Random r = new Random();
        for (int i = 0; i < arr.Length; i++)
        {
            int index = r.Next(arr.Length);
            int temp = arr[i];
            arr[i] = arr[index];
            arr[index] = temp;
        }
    }

    /// <summary>
    /// 最开始生成五个笔画方块
    /// </summary>
    /// <param name="hanzi"></param>生成的哪个汉字的方块
    /// <param name="animator"></param>笔画修改的是哪个动画状态机
    public void MadeFiveStroke(Hanzi hanzi,Animator animator)
    {
        int Num = HanziToNum(hanzi);


        Sort(ref offetList);
        if (Num >= 5)
        {
            for (int i = 0; i < 5; i++)
            {

                MadeStroke(hanzi, animator, offetList[i]);
            }
        }
        else
        {
            for (int i = 0; i < Num; i++)
            {
                MadeStroke(hanzi, animator, offetList[i]);
            }
        }
        
    }

    /// <summary>
    /// 在撞击后生成单个方块
    /// </summary>
    /// <param name="hanzi"></param>汉字的类型
    /// <param name="animator"></param>方块改变的动画状态机
    /// <param name="OffetNum"></param>/方块位置的偏移量
    public void MadeOneStroke(Hanzi hanzi, Animator animator,int OffetNum)
    {
        int Num = HanziToNum(hanzi);
        if (HanziNum > Num)  return;
        MadeStroke(hanzi,animator,OffetNum);
    }

    /// <summary>
    /// 生成笔画脚本,本身
    /// </summary>
    /// <param name="hanzi"></param>汉字类型
    /// <param name="animator"></param>汉字对应的动画
    /// <param name="OffetNum"></param>汉字生成位置的偏移量
    /// <param name="isLast"></param>是否是最后一个
    private void MadeStroke(Hanzi hanzi, Animator animator, int offetnum)
    {
        
        int Num = HanziToNum(hanzi);//获取汉字的笔画数

        selectStroke(hanzi, offetnum);//按照偏移量和类型生成特定方块

        //判断当前笔画方块是否是最后一个进行特定初始化
        if (HanziNum == Num)//当当前笔画和实际笔画数相同是就表示这次初始化是最后一个了
        {
            stroke.Init(HanziNum, hanzi, animator, offetnum,true);
        }
        else
        {
            stroke.Init(HanziNum, hanzi, animator, offetnum, false);
        }

        HanziNum++;
    }

    /// <summary>
    /// 根据汉字标识位和方块偏移量标识  生成特定  笔画方块
    /// </summary>
    /// <param name="hanzi"></param>汉字标识位
    /// <param name="offetnum"></param>方块偏移量标识
    private void selectStroke(Hanzi hanzi, int offetnum)
    {
        Vector3 pos = Base + offetnum * offet;//生成的坐标是基准点加偏移量
        switch (hanzi)
        {
            case Hanzi.Jian:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Jian_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Chi:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Chi_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Bu:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Bu_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0,-180,0)).GetComponent<Stroke>();
                break;
            case Hanzi.Xie:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Xie_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Xu:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Xu_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Xing:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Xing_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Qiu:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Qiu_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Jiao:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Jiao_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Zhi:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Zhi_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Cuo:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Cuo_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Neng:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Neng_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Gai:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Gai_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Ying:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Ying_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Cai:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Cai_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
            case Hanzi.Shi:
                stroke = GameObject.Instantiate(GameManager.Instance.gameConf.Shi_Bh[HanziNum - 1],
                    pos, Quaternion.Euler(0, -180, 0)).GetComponent<Stroke>();
                break;
        }
    }

    /// <summary>
    /// 笔画开启方法
    /// </summary>
    /// <param name="hanzi"></param>当前运行结束的是哪个汉字
    public void ChangeHanzi(Hanzi hanzi)
    {
        HanziNum = 1;
        switch (hanzi)
        {
            case Hanzi.Start://当位开始标识位开启笔画时   
                switch (GameManager.Instance.Guanka)  //按照当前游戏的类型启动不同的汉字
                {
                    case Guanka.YGYS:
                        MadeFiveStroke(Hanzi.Jian, EnvManager.Instance.Animators[0]);
                        break;
                    case Guanka.YMZZ:
                        MadeFiveStroke(Hanzi.Ying, EnvManager.Instance.Animators[0]);
                        break;
                    case Guanka.FJQZ:
                        MadeFiveStroke(Hanzi.Zhi, EnvManager.Instance.Animators[0]);
                        break;
                    case Guanka.CMLX:
                        MadeFiveStroke(Hanzi.Xu, EnvManager.Instance.Animators[0]);
                        break;
                }
                break;
            //接下来都是启动下一个字的笔画生成
            case Hanzi.Jian:
                MadeFiveStroke(Hanzi.Chi, EnvManager.Instance.Animators[1]);
                break;
            case Hanzi.Chi:
                MadeFiveStroke(Hanzi.Bu, EnvManager.Instance.Animators[2]);
                break;
            case Hanzi.Bu:
                MadeFiveStroke(Hanzi.Xie, EnvManager.Instance.Animators[3]);
                break;
            case Hanzi.Xie:
                NamePlane.Instance.ShowSecond();
                break;



            case Hanzi.Zhi:
                MadeFiveStroke(Hanzi.Cuo, EnvManager.Instance.Animators[1]);
                break;
            case Hanzi.Cuo:
                MadeFiveStroke(Hanzi.Neng, EnvManager.Instance.Animators[2]);
                break;
            case Hanzi.Neng:
                MadeFiveStroke(Hanzi.Gai, EnvManager.Instance.Animators[3]);
                break;
            case Hanzi.Gai:
                NamePlane.Instance.ShowSecond();
                break;

            case Hanzi.Xu:
                MadeFiveStroke(Hanzi.Xing, EnvManager.Instance.Animators[1]);
                break;
            case Hanzi.Xing:
                MadeFiveStroke(Hanzi.Qiu, EnvManager.Instance.Animators[2]);
                break;
            case Hanzi.Qiu:
                MadeFiveStroke(Hanzi.Jiao, EnvManager.Instance.Animators[3]);
                break;


            case Hanzi.Ying:
                MadeFiveStroke(Hanzi.Cai, EnvManager.Instance.Animators[1]);
                break;
            case Hanzi.Cai:
                MadeFiveStroke(Hanzi.Shi, EnvManager.Instance.Animators[2]);
                break;
            case Hanzi.Shi:
                MadeFiveStroke(Hanzi.Jiao, EnvManager.Instance.Animators[3]);
                break;
            case Hanzi.Jiao:
                NamePlane.Instance.ShowSecond();
                break;
        }
    }
}
