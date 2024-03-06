using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Poetry1 : MonoBehaviour
{
    //诗句第二个背景转变
    public GameObject Two;
    //定义风的动画
    public Animator Anim;
    //定义风的实例
    public GameObject Wind;
    //刮风的声音
    public AudioSource CloudAudio;
    private bool IsInput = true;
    private bool IsInput1 = false;
    private bool isPlay = false;
    //改变诗句出现的时间
    public float Times = 0.02f;
    //通关菜单
    public GameObject Tips;
    //返回主菜单,下一关按钮
    public Button btn1, btn2, btn3;
    //输入春风后,场景诗句中显示春风二字
    public GameObject Spring;
    //定义提示实例
    public GameObject Help;
    //定义诗句出现的白底背景
    public GameObject Tip;
    //计数器
    public float[] FillTime = { 2, 2, 4, 4, 4, 4 };
    public float[] timer = { 2, 2, 4, 4, 4, 4, };
    //检测输入文本
    private InputField text1;
    //定义文本遮罩数组
    public Image[] CancelFill;
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
    private void Start()
    {
        isFinish = true;
        text1 = transform.GetComponentInChildren<InputField>();//获取InputField组件
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

            SceneManager.LoadScene(7);
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFinish)
        {
            PoetryOccur.SetActive(true);
            isFinish = false;
            StopAllCoroutines();
            Invoke("PointChange", 2f);
            IsInput1 = true;
        }

        if (text1.text == null) return;
        //判断输入文本是否为春风二字,
        if (text1.text == "春风" && IsInput)
        {
            Spring.SetActive(true);
            Wind.SetActive(true);
            Anim.GetComponent<Animator>().enabled = true;
            CloudAudio.Play();//播放刮风音效
            Anim.Play("Wind2");//播放刮风动画
            Invoke("CloseAnim", 0.8f);
            Invoke("SetTwo", 1.8f);//延时切换第二个画面
            IsInput = false;//控制循环进入一次
        }
        //时刻监听点击提示按钮
        HelpBtn();
        //按下ESC游戏暂停界面,用于返回主菜单或者退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc.SetActive(true);
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
    //延时调用Wind动画
    public void CloseAnim()
    {
        Wind.SetActive(false);
    }
    //输入春风后,显示第二个画面
    public void SetTwo()
    {
        Two.SetActive(true);
        Invoke("FillManager", 4f);
    }
    //该方法用于调用协程
    private void FillManager()
    {
        isFinish = false;
        Tip.SetActive(true);
        StartCoroutine(Fill0X(0));
    }
    //使用协程将所有诗句的出现,通过一个方法实现
    IEnumerator Fill0X(int Num)
    {
        print("使用了携程");
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
        PoetryText.SetActive(false);
        Mouse.SetActive(true);
    }
    private void PointChange1()
    {
        Mouse1.SetActive(true);
    }
}
