using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Poetry2 : MonoBehaviour
{
    //计数器数组
    public float[] FillTime = { 2, 2, 4, 4, 4, 4 };
    public float[] timer = { 2, 2, 4, 4, 4, 4, };
    private bool IsInput1 = false;
    private bool isPlay = false;
    //改变诗句出现的时间
    public float Times = 0.02f;
    public Image[] CancelFill;
    //通关菜单
    public GameObject Tips;
    //返回主菜单,下一关按钮
    public Button btn1, btn2, btn3;
    //定义提示实例
    public GameObject Help;
    //定义诗句出现的白底背景
    public GameObject Tip;
    //点击空白继续游戏
    public GameObject Mouse;
    public Button Mousedown;
    //点击空白继续游戏,背景为透明色
    public GameObject Mouse1;
    public Button Mousedown1;
    //游戏暂停界面,用于返回主菜单或者退出游戏
    public GameObject Esc;
    public Button ESCBtn1, ESCBtn2, ESCBtn3;
    //按下空格出现所有诗句
    public GameObject PoetryOccur;
    public GameObject PoetryText;
    public bool isFinish = true;
    //定义计数器
    public int index = 0;

    private void Start()
    {
        isFinish = true;
        //点击鼠标左键后,出现通关界面
        Mousedown.onClick.AddListener(() =>
        {
            Mouse.SetActive(false);
            if (IsInput1)
            {
                Tips.SetActive(true);
            }
        });
        //点击鼠标左键后,关闭提示界面
        Mousedown1.onClick.AddListener(() =>
        {
            Mouse1.SetActive(false);
            Help.SetActive(false);
        });
        //点击返回主菜单
        btn1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        //点击进入下一关
        btn2.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(8);
        });
        //点击返回主菜单
        ESCBtn1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        //退出游戏
        ESCBtn2.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        });
        //返回游戏按钮
        ESCBtn3.onClick.AddListener(() =>
        {
            Esc.SetActive(false);
        });
    }
    private void Update()
    {
        //时刻监听点击提示按钮
        HelpBtn();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&! isFinish)
        {
            PoetryOccur.SetActive(true);
            isFinish = false;
            StopAllCoroutines();
            Invoke("PointChange", 2f);
            IsInput1 = true;
        }
    }

    //定义触发事件,判断是否正确移动场景内交互的物体
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到之后,计数器自增
        if (collision.tag == "lotus")
        {
            index++;
        }
        //延时调用诗句出现界面
        if (index >= 1)
        {
            Invoke("TipOccur", 1.25f);
        }
    }

    //提示按钮,点击出现场景提示
    public void HelpBtn()
    {
        btn3.onClick.AddListener(() =>
        {
            Help.SetActive(true);
            PointChange1();
        });
    }
    //该方法用于调用协程
    private void TipOccur()
    {
        Tip.SetActive(true);
        isFinish = false;
        StartCoroutine(Fill0X(0));
    }
    //使用协程将所有诗句的出现,通过一个方法实现
    IEnumerator Fill0X(int Num)
    {
        PoetryText.SetActive(true);
        yield return 0;
        isPlay = true;
        while (isPlay)
        {
            yield return new WaitForSeconds(Times);
            //使timer根据时间减少
            FillMain(Num);
        }
        //如果没有到最后一句诗,则依次调用协程
        if (Num != 5 && !isFinish)
        {
            Num++;
            StartCoroutine(Fill0X(Num));
        }
        //当最后一句诗出现后,使用延时调用方法,出现通关界面
        else
        {
            Invoke("PointChange", 2f);
            IsInput1 = true;
        }
    }
    //控制诗句出现的速度
    private void FillMain(int Num)
    {
        timer[Num] -= Time.fixedDeltaTime;
        CancelFill[Num].fillAmount = timer[Num] / FillTime[Num];
        if (timer[Num] <= 0)
        {
            isPlay = false;
        }
    }
    /// <summary>
    /// 这里写第一个场景转变到第二个场景
    /// </summary>
    private void PointChange()
    {
        Mouse.SetActive(true);
        PoetryText.SetActive(false);
    }
    private void PointChange1()
    {
        Mouse1.SetActive(true);
    }
}
