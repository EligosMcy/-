using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecondPoetry : MonoBehaviour
{
    //诗句第二个背景转变
    public GameObject Two;
    //定义风的动画
    public Animator Anim;
    public GameObject Wind;
    public AudioSource CloudAudio;
  //  private bool IsInput = true;
   // private bool IsInput1 = false;
    private bool isPlay = false;
    //改变诗句出现的时间
    public float Times = 0.02f;



    public GameObject Tip;
    //计数器
    public float[] FillTime = { 2, 2, 4, 4, 4, 4 };
    public float[] timer = { 2, 2, 4, 4, 4, 4, };

    //检测输入文本
    private InputField text1;
    //定义文本遮罩数组
    public Image[] CancelFill;

    public GameObject Mouse;
    public Button Mousedown;

    private void Start()
    {
        text1 = transform.GetComponentInChildren<InputField>();

        Mousedown.onClick.AddListener(() =>
        {
            Mouse.SetActive(false);
            SceneManager.LoadScene(1);
        });

    }
    //void Update()
    //{
    //    if (text1.text == null) return;

    //    if (text1.text == "春风" && IsInput)
    //    {
    //        //Wind.SetActive(true);
    //        //Anim.GetComponent<Animator>().enabled = true;
    //        //CloudAudio.Play();
    //        //Anim.Play("Wind2");
    //        //Invoke("CloseAnim", 0.8f);
    //        Invoke("SetTwo", 1.8f);
    //        IsInput = false;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("SetTwo", 1.2f);
        }
    }
    //延时调用Wind动画
    //public void CloseAnim()
    //{
    //    Wind.SetActive(false);
    //}
    //显示第二个画面
    public void SetTwo()
    {
       // Two.SetActive(true);
        Invoke("FillManager", 4f);
    }
    private void FillManager()
    {
        Tip.SetActive(true);
        StartCoroutine(Fill0X(0));
    }
    IEnumerator Fill0X(int Num)
    {
        yield return 0;
        isPlay = true;
        //isfirst = true;
        while (isPlay)
        {
            yield return new WaitForSeconds(Times);
            //使timer根据时间减少
            FillMain(Num);
        }
        if (Num != 5)
        {
            Num++;
            StartCoroutine(Fill0X(Num));
        }
        else
        {
            Invoke("PointChange", 2f);
        }
    }
    private void FillMain(int Num)
    {
        timer[Num] -= Time.deltaTime;
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
    }
}
