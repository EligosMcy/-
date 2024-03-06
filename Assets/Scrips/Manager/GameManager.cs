using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Guanka
{
    Start,
    YGYS,
    YMZZ,
    CMLX,
    FJQZ
}


// switch (GameManager.Instance.Guanka)
// {
//     case Guanka.YGYS:
//
//
//         break;
//     case Guanka.YMZZ:
//
//         break;
//     case Guanka.FJQZ:
//
//
//         break;
//     case Guanka.CMLX:
//
//
//         break;
//
// }


// base.Effect();
// transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.4f, 12, 0.5f);  长方体动效
//
// base.Effect();
// transform.DOPunchScale(new Vector3(-0.2f, 0, 0), 0.4f, 12, 0.5f);   方块动效

//AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.Jump);


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameConf gameConf;
    public Player Player;
    public Guanka Guanka;
    public static GameManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        instance = this;
        gameConf = Resources.Load<GameConf>("GameConf");
        if (GameObject.FindWithTag("Player") != null)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

    }

    private void Start()
    {
    }

    public void OpenPlan(int Num)
    {
        switch (Guanka)
        {
            case Guanka.YGYS:
                switch (Num)
                {
                    case 1:
                        StartCoroutine(First_1());
                        break;
                    case 2:
                        StartCoroutine(First_2());
                        break;
                    case 3:
                        StartCoroutine(First_3());
                        break;
                    case 4:
                        StartCoroutine(First_4());
                        break;
                    case 5:
                        StartCoroutine(First_5());
                        break;
                } 
                break;
            case Guanka.YMZZ:
                switch (Num)
                {
                    case 1:
                        StartCoroutine(Second_1());
                        break;
                    case 2:
                        StartCoroutine(Second_2());
                        break;
                    case 3:
                        StartCoroutine(Second_3());
                        break;
                    case 4:
                        StartCoroutine(Second_4());
                        break;
                    case 5:
                        StartCoroutine(Second_5());
                        break;
                }
                break;
            case Guanka.FJQZ:
                switch (Num)
                {
                    case 1:
                        StartCoroutine(Third_1());
                        break;
                    case 2:
                        StartCoroutine(Third_2());
                        break;
                    case 3:
                        StartCoroutine(Third_3());
                        break;
                    case 4:
                        StartCoroutine(Third_4());
                        break;
                    case 5:
                        StartCoroutine(Third_5());
                        break;
                }
                break;
            case Guanka.CMLX:
                switch (Num)
                {
                    case 1:
                        StartCoroutine(Fourth_1());
                        break;
                    case 2:
                        StartCoroutine(Fourth_2());
                        break;
                    case 3:
                        StartCoroutine(Fourth_3());
                        break;
                    case 4:
                        StartCoroutine(Fourth_4());
                        break;
                    case 5:
                        StartCoroutine(Fourth_5());
                        break; 
                    case 6:
                        StartCoroutine(Fourth_6());
                        break;
                }
                break;
            
            
        }
    
    }
    
    /// <summary>
    /// 批量处理场景
    /// </summary>
    /// <param name="Start"></param>开始编号
    /// <param name="Exit"></param>结束编号
    /// <param name="isShow"></param>展示，还是影藏
    private void ChangEnv(int Start, int Exit,bool isShow = true)
    {
        if (isShow)
        {
            for (int i = Start; i <= Exit; i++)
            {
                EnvManager.Instance.ShowEnv(i);
            }
        }
        else
        {
            for (int i = Start; i <= Exit; i++)
            {
                EnvManager.Instance.HideEnv(i);
            }
        }
        
    }

    /// <summary>
    /// 处理单个场景
    /// </summary>
    /// <param name="Num"></param>
    private void ChangEnv(int Num,bool isShow=true)
    {
        if (isShow)
        {
            EnvManager.Instance.ShowEnv(Num);
        }
        else
        {
            EnvManager.Instance.HideEnv(Num);
        }
        
    }



    /// <summary>
    /// 处理多条线
    /// </summary>
    /// <param name="Start"></param>开始
    /// <param name="Exit"></param>结束
    /// <param name="line"></param>线类型
    /// <param name="isShow"></param>显示还是隐藏
    private void ChangLine(int Start, int Exit, LineName line ,bool isShow = true)
    {
        if (isShow)
        {
            for (int i = Start; i <= Exit; i++)
            {
                LineManager.Instance.ShowLine(line, i);
            }
        }
        else
        {
            for (int i = Start; i <= Exit; i++)
            {
                LineManager.Instance.HideLine(line, i);
            }
        }
    }

    /// <summary>
    /// 处理单条线
    /// </summary>
    /// <param name="Num"></param>线的编号
    /// <param name="line"></param>线的类型
    /// <param name="isShow"></param>是否显示还是隐藏
    private void ChangLine(int Num, LineName line, bool isShow = true)
    {
        if (isShow)
        {
            LineManager.Instance.ShowLine(line, Num);
        }
        else
        {
            LineManager.Instance.HideLine(line,Num);
        }
    }



    /// <summary>
    /// 批量处理抓取盒
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="Exit"></param>
    private void ChangPickUPBox(int Start, int Exit,bool isShow = true)
    {
        if (isShow)
        {
            for (int i = Start; i <= Exit; i++)
            {
                PickBoxManager.Instance.ShowPick(i);
            }
        }
        else
        {
            for (int i = Start; i <= Exit; i++)
            {
                PickBoxManager.Instance.HidePick(i);
            }
        }
        
    }

    /// <summary>
    /// 处理单个抓取盒
    /// </summary>
    /// <param name="Num"></param>
    private void ChangPickUPBox(int Num, bool isShow = true)
    {
        if (isShow)
        {
            PickBoxManager.Instance.ShowPick(Num);
        }
        else
        {
            PickBoxManager.Instance.HidePick(Num);
        }
        
    }



    /// <summary>
    /// 批量处理放置盒
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="Exit"></param>
    private void ChangFlagBox(int Start, int Exit,bool isShow = true)
    {
        if (isShow)
        {
            for (int i = Start; i <= Exit; i++)
            {
                PickBoxManager.Instance.ShowFlags(i);
            }
        }
        else
        {
            for (int i = Start; i <= Exit; i++)
            {
                PickBoxManager.Instance.HideFlags(i);
            }
        }
        

    }
    
    /// <summary>
    /// 处理单个放置盒
    /// </summary>
    /// <param name="Num"></param>
    private void ChangFlagBox(int Num, bool isShow = true)
    {
        if (isShow)
        {
            PickBoxManager.Instance.ShowFlags(Num);
        }
        else
        {
            PickBoxManager.Instance.HideFlags(Num);
        }
    }



    /// <summary>
    /// 开启显示文字效果
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private float ShowType(int num)
    {
        return TypeManager.Instance.ShowType(num);
    }

    /// <summary>
    /// 隐藏文字
    /// </summary>
    /// <param name="num"></param>
    private void HideType(int Start, int Exit)
    {
        for (int i = Start; i <= Exit; i++)
        {
            TypeManager.Instance.HideType(i);
        }
    }

    /// <summary>
    /// 修改玩家的运动状态
    /// </summary>
    /// <param name="isMove"></param>
    /// <param name="isShow"></param>
    private void ChangPlayer(bool isMove, bool isShow = true)
    {
        Player.RevampPlayer(isMove,isShow);
    }

    #region 愚公移山
    
    //云，回答框，绿山，房子，小的愚公，,新房子,大山 大愚公空，愚公，门  环境显示的顺序  问号指针长条
    //  1  2      3     4     5         6    7    8       9    10                   11,12,13
    IEnumerator First_1()
    {
        yield return 0;
        //云显示,回答框显示,三条线显示
        ChangPlayer(false);
        ChangEnv(1);//显示云
        ChangEnv(2);//显示回答框
        ChangLine(1,LineName.CloudLine);//显示第一段云线
        ChangLine(1, 3, LineName.WordsLine);//显示前三条线
        ChangLine(1, 3, LineName.ChatLine);//显示前三条回答框的线
        yield return new WaitForSeconds(ShowType(1));
        ChangEnv(3);//显示绿山
        ChangPickUPBox(1);
        ChangPickUPBox(5);
        yield return new WaitForSeconds(ShowType(2));
        ChangPlayer(true);
        yield return new WaitForSeconds(ShowType(3));//依次开启前三行文字的书写
    }
    
    IEnumerator First_2()
    {
        yield return 0;
        ChangEnv(4,5);
        yield return new WaitForSeconds(1);//等待一秒
        ChangLine(4, 5, LineName.WordsLine);//显示4,5条线
        ChangPickUPBox(2);
        ChangPickUPBox(6);
        yield return new WaitForSeconds(ShowType(4));//依次开启前四行文字的书写
        yield return new WaitForSeconds(ShowType(5));//依次开启前五行文字的书写3
        ChangLine(1,LineName.WordsLineWall);
    }
    
    IEnumerator First_3()
    {
        yield return 0;
        ChangEnv(4, false);
        ChangEnv(6);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(6,8,LineName.WordsLine);
        ChangPickUPBox(3);
        ChangPickUPBox(7);
        yield return new WaitForSeconds(ShowType(6));//依次开启前四行文字的书写
        ChangEnv(7);
        yield return new WaitForSeconds(ShowType(7));//依次开启前四行文字的书写
        ChangEnv(5, false);
        ChangEnv(8);
        ChangEnv(11);
        ChangLine(2, LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(8));//依次开启前四行文字的书写
    }
    
    IEnumerator First_4()
    {
        yield return 0;
        ChangEnv(8,false);
        ChangEnv(11,false);
        ChangEnv(12);
        ChangEnv(9);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(2, LineName.WordsLineWall,false);
        ChangLine(9,12,LineName.WordsLine);
        ChangLine(3,LineName.WordsLineWall);
        ChangLine(4, LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(9));//显示第八行文字和横线
        yield return new WaitForSeconds(ShowType(10));//显示第八行文字和横线
        yield return new WaitForSeconds(ShowType(11));//显示第八行文字和横线
        yield return new WaitForSeconds(ShowType(12));//显示第八行文字和横线
        ChangFlagBox(4,true);
        ChangEnv(13);
    }

    IEnumerator First_5()
    {
        yield return 0;
        ChangEnv(7,false);
        ChangLine(4, LineName.WordsLineWall,false);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangEnv(10);
        ChangLine(13,LineName.WordsLine);
    }
    
    
    #endregion
    
    #region 拔苗助长
    
    //云，树，山1，山2  拾取框1  拾取框2  人1  人2  米1  米2  米3   草地1  草地2  水1  水2  第二关的门  问号  指向1  指向2
    //1   2  3    4    5       6        7    8   9    10   11    12     13    14   15      16       17    18   19
    IEnumerator Second_1()
    {
        ChangLine(1, 2, LineName.WordsLine);
        ChangPlayer(false);
        ChangEnv(1);
        ChangEnv(5);
        yield return new WaitForSeconds(ShowType(1));//显示第一行文字和横线
        ChangLine(1,3,LineName.ChatLine);
        ChangLine(3,4, LineName.WordsLine);
        ChangPickUPBox(1,2);
        ChangPickUPBox(5,6);
        ChangEnv(3);
        ChangEnv(14);
        ChangEnv(12);
        ChangEnv(17);
        yield return new WaitForSeconds(ShowType(2));//显示第一行文字和横线
        ChangLine(1,LineName.WordsLineWall);
        ChangPlayer(true);
    }
    
    IEnumerator Second_2()
    {
        yield return 0;
        ChangEnv(2);
        ChangEnv(7);
        ChangEnv(17,false);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的

    }
    IEnumerator Second_3()
    {
        yield return 0;
        ChangEnv(9);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(3,4,LineName.WordsLine);
        yield return new WaitForSeconds(ShowType(3));
        ChangPickUPBox(3);
        yield return new WaitForSeconds(ShowType(4));
    }
    IEnumerator Second_4()
    {
        yield return 0;
        ChangEnv(6);
        ChangEnv(7,false);
        ChangEnv(8);
        ChangEnv(9,false);
        ChangEnv(10);
        yield return new WaitForSeconds(1); //这一秒是用来停下来显示改变效果的
        ChangLine(5,7,LineName.WordsLine);
        ChangLine(2,3,LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(5));
        ChangEnv(8, false);
        ChangPickUPBox(4);
        ChangPickUPBox(7);
        ChangLine(4, 6, LineName.ChatLine);
        yield return new WaitForSeconds(ShowType(6));
        yield return new WaitForSeconds(ShowType(7));
        yield return new WaitForSeconds(ShowType(8));
    }
    
    IEnumerator Second_5()
    {
        yield return 0;
        ChangEnv(2,false);
        ChangEnv(10,false);
        ChangEnv(3,false);
        ChangEnv(11);
        ChangEnv(13);
        ChangEnv(15);
        ChangEnv(4);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(3,LineName.WordsLineWall,false);
        ChangEnv(18,19);
        ChangLine(8,LineName.WordsLine);
        ChangEnv(16);
    }
    
    #endregion
    
    #region 负荆请罪
    
    // 背景1   树1  廉颇01_1   廉颇01_2
    // 1       2   3          4
    
    // 背景2   树2  草2        房子2      廉颇02_1   廉颇02_2
    // 5       6    7         8          9          10
    
    // 背景03  廉颇03_1    蔺相如03_1     廉颇03_2    蔺相如03_2   第二关的门
    // 11      12          13            14         15           16

    //问号,指向,,短指向，短指向,指向
    //17   18    19     20     21
    
    IEnumerator Third_1()
    {
        yield return 0;
        ChangPlayer(false);
        ChangEnv(1);
        ChangEnv(2);
        ChangEnv(17);
        ChangLine(1,5,LineName.WordsLine);
        ChangLine(1,LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(1));//显示第一行文字和横线
        ChangPickUPBox(1);
        ChangPickUPBox(5);
        ChangLine(1,2,LineName.ChatLine);
        yield return new WaitForSeconds(ShowType(2));//显示第二行文字和横线
        yield return new WaitForSeconds(ShowType(3));//显示第三行文字和横线
        ChangPlayer(true);
    }    
    IEnumerator Third_2()
    {
        yield return 0;
        ChangEnv(3);
        ChangEnv(17,false);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangPickUPBox(2);
        ChangPickUPBox(6);
        ChangLine(2, LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(4));//显示第四行文字和横线
        yield return new WaitForSeconds(ShowType(5));//显示第五行文字和横线

    }    
    IEnumerator Third_3()
    {
        yield return 0;
        ChangEnv(3,false);
        ChangEnv(4);
        ChangEnv(19);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(1,LineName.CloudLine);
        ChangEnv(5,7);
        ChangLine(6, 12, LineName.WordsLine);
        ChangLine(3, LineName.WordsLineWall);
        ChangEnv(18);
        yield return new WaitForSeconds(ShowType(6));//显示第六行文字和横线
        ChangLine(3, LineName.ChatLine);
        ChangPickUPBox(3);
        ChangPickUPBox(7);
        yield return new WaitForSeconds(ShowType(7));//显示第七行文字和横线
        yield return new WaitForSeconds(ShowType(8));//显示第八行文字和横线
        ChangEnv(8,9);
        yield return new WaitForSeconds(ShowType(9));//显示第九行文字和横线
        yield return new WaitForSeconds(ShowType(10));//显示第十行文字和横线
        

    }    
    IEnumerator Third_4()
    {
        ChangEnv(9, false);
        ChangEnv(10);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        yield return new WaitForSeconds(ShowType(11));//显示第十一行文字和横线
        yield return new WaitForSeconds(ShowType(12));//显示第十二行文字和横线
        ChangEnv(20);
        ChangEnv(11);
        ChangLine(13, 18, LineName.WordsLine);
        yield return new WaitForSeconds(ShowType(13));//显示第十三行文字和横线
        ChangLine(4, LineName.ChatLine);
        ChangPickUPBox(4);
        ChangPickUPBox(8);
        yield return new WaitForSeconds(ShowType(14));//显示第十四行文字和横线
        yield return new WaitForSeconds(ShowType(15));//显示第十五行文字和横线
        ChangEnv(12,13);
        ChangLine(4,LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(16));//显示第十六行文字和横线
        yield return new WaitForSeconds(ShowType(17));//显示第十七行文字和横线
        yield return new WaitForSeconds(ShowType(18));//显示第十八行文字和横线
        yield return 0;
    }    
    IEnumerator Third_5()
    {
        yield return 0;
        ChangEnv(12,13,false);
        ChangEnv(14,15);
        yield return new WaitForSeconds(1);//这一秒是用来停下来显示改变效果的
        ChangLine(2,LineName.CloudLine);
        ChangEnv(21);
        ChangEnv(16); 

        //隐藏中间部分
        ChangLine(6,12,LineName.WordsLine,false);
        HideType(6,12);
        ChangEnv(5,10,false);
        ChangLine(3,LineName.ChatLine,false);
        ChangFlagBox(3,false);
        ChangLine(3,LineName.WordsLineWall,false);
        ChangEnv(19,false);
        ChangEnv(20,false);
        ChangPickUPBox(7,false);
    }

    #endregion

    #region 程门立雪


    //背景1       地板1     屏风        香炉   老师     朋友      杨时
    //1             2       3           4       5       6           7

    //背景2        雪        人1      人2
    //    8           9       10      11

    //天空        地面      门1       树       雪       朋友1     杨时1         朋友2     杨时2        门2  门  
    //    12        13      14      15         16      17          18          19      20          21     22

    //指向，指向，问号，短指向 指向问号，指向问号
    //23     24   25  26      27 28    29  30




    IEnumerator Fourth_1()
    {
        yield return 0;
        ChangPlayer(false);
        ChangEnv(1,2);
        ChangLine(1,5,LineName.WordsLine);
        ChangLine(1, 2, LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(1));
        ChangLine(1,LineName.ChatLine);
        ChangPickUPBox(1);
        ChangPickUPBox(6);
        yield return new WaitForSeconds(ShowType(2));
        ChangEnv(3);
        ChangPlayer(true);
        yield return new WaitForSeconds(ShowType(3));
        ChangEnv(4);
        yield return new WaitForSeconds(ShowType(4));
        ChangEnv(5);
        yield return new WaitForSeconds(ShowType(5));
        ChangEnv(6);

    }
    IEnumerator Fourth_2()
    {
        yield return 0;
        ChangEnv(7);
        yield return new WaitForSeconds(1);
        ChangLine(1,LineName.CloudLine);
        ChangLine(2,LineName.WordsLineWall,false);
        ChangEnv(23,24);
        ChangLine(6,8,LineName.WordsLine);
        ChangLine(3,LineName.WordsLineWall);
        yield return new WaitForSeconds(ShowType(6));//显示第六行文字和横线
        ChangEnv(8);
        ChangEnv(25);
        ChangLine(2, LineName.ChatLine);
        ChangPickUPBox(2);
        ChangPickUPBox(7);
        yield return new WaitForSeconds(ShowType(7));//显示第六行文字和横线
        yield return new WaitForSeconds(ShowType(8));//显示第六行文字和横线


    }
    IEnumerator Fourth_3()
    {
        yield return 0;
        ChangEnv(9);
        yield return new WaitForSeconds(1);
        ChangLine(9, LineName.WordsLine);
        ChangPickUPBox(3);
        ChangPickUPBox(8);
        yield return new WaitForSeconds(ShowType(9));//显示第六行文字和横线
        ChangLine(4, LineName.WordsLineWall);
    }
    IEnumerator Fourth_4()
    {
        yield return 0;
        ChangEnv(10,11);
        ChangEnv(25,false);
        yield return new WaitForSeconds(1);
        ChangLine(4, LineName.WordsLineWall, false);
        ChangEnv(26);
        ChangLine(2, LineName.CloudLine);
        ChangEnv(12,13);
        ChangLine(10,15,LineName.WordsLine);
        yield return new WaitForSeconds(ShowType(10));//显示第六行文字和横线
        ChangEnv(14, 16);
        ChangLine(3, LineName.ChatLine);
        ChangPickUPBox(4);
        ChangPickUPBox(9);
        yield return new WaitForSeconds(ShowType(11));//显示第六行文字和横线
        yield return new WaitForSeconds(ShowType(12));//显示第六行文字和横线

    }
    IEnumerator Fourth_5()
    {
        yield return 0;
        ChangEnv(17, 18);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(ShowType(13));//显示第六行文字和横线
        ChangPickUPBox(5);
        ChangPickUPBox(10);
        yield return new WaitForSeconds(ShowType(14));//显示第六行文字和横线
    }
    IEnumerator Fourth_6()
    {
    
        yield return 0;
        ChangEnv(17, 18, false);
        ChangEnv(19,20);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(ShowType(15));//显示第六行文字和横线
        ChangEnv(14,false);
        ChangEnv(21);
        ChangEnv(27,30);
        yield return new WaitForSeconds(1);
        ChangEnv(19,20,false);
        ChangLine(3,LineName.CloudLine);
        ChangEnv(22);

        // //隐藏空余的部分
        ChangLine(6,9,LineName.WordsLine,false);
        HideType(6,9);
        ChangEnv(8,11,false);
        ChangLine(2,LineName.ChatLine, false);
        ChangLine(2,LineName.CloudLine, false);
        ChangEnv(26,false);
        ChangFlagBox(2,3,false);
        ChangPickUPBox(7, 8, false);
    }
    #endregion
}