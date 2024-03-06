using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class NamePlane : MonoBehaviour
{
    private static NamePlane instance;
    public static NamePlane Instance { get => instance; }
    private Animator animator;
    private GameObject Second;
    private GameObject Esc;
    private GameObject Poetry;
    private GameObject Bg;
    private AudioSource audioSource;
    private Slider slider;
    private bool IsStart = false;
    private bool IsEsc = false;
    private bool isSecond = false;
    public bool isPoetry = false;
    public float timers = 0.5f;
    private Image Image;
    private AudioSource myAudioSource;
    public Animator Animator
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            return animator;
        }
    }

    private void Awake()
    {
        instance = this;
        myAudioSource = transform.GetComponent<AudioSource>();
        Bg = transform.Find("Bg").gameObject;
        Second = transform.Find("Bg/Second").gameObject;
        Esc = transform.Find("Bg/ESC").gameObject;
        slider = transform.Find("Bg/ESC/Music/Slider").gameObject.GetComponent<Slider>();
        if (isPoetry)
        {
            Poetry = transform.Find("Bg/Poetry").gameObject;
        }
        Image = gameObject.GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (Poetry != null)
        {
            Changother(Poetry, false,0.01f);
        }
        Changother(Esc,false, 0.01f);
        Changother(Second, false, 0.01f);
        AudioManager.Instance.StopMusic();//关闭背景音乐
        audioSource.Play();
        GameManager.Instance.Player.RevampPlayer(false, false,false);
        slider.value = AudioManager.Instance.AudioSource.volume;
        myAudioSource.volume = slider.value;
        slider.onValueChanged.AddListener((temp)=>
        {
            AudioManager.Instance.AudioSource.volume = AudioManager.Instance.AudioNum * temp;
            myAudioSource.volume = temp;
            AudioManager.Instance.AudioSource.volume = temp;
            AudioManager.Instance.Audionvalue = temp;

        }
        );
    }

    void Update()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            CloseUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !IsEsc && IsStart && !isSecond)
        {
            ShowEsc();
        }
    }

    private void ChangeBg(bool isShow)
    {
        Bg.gameObject.SetActive(isShow);
        Image.enabled = isShow;
    }

    private void Changother(GameObject other,bool isShow,float times,Action Topath = null)
    {
        if (isShow)
        {
            other.transform.GetComponent<RectTransform>().DOAnchorPos3DX(0, times).OnComplete(() =>
                {
                    if (Topath != null)
                    {
                        Topath();
                    }
                }
            );
        }
        else
        {
            other.transform.GetComponent<RectTransform>().DOAnchorPos3DX(1500, times).OnComplete(() =>
            {
                if (Topath != null)
                {
                    Topath();
                }
            }
            );
        }    
    }

    public void CloseUI()
    {
        ChangeBg(false);
        GameManager.Instance.Player.RevampPlayer(true, true,true);
        Animator.gameObject.SetActive(false);
        GameManager.Instance.OpenPlan(1);
        AudioManager.Instance.PlayMusic();//开启背景音乐
        audioSource.Pause();
        IsStart = true;
    }


    public void ShowSecond()
    {
        isSecond = true;
        ChangeBg(true);
        audioSource.Play();
        AudioManager.Instance.StopMusic();
        Changother(Esc, false, timers);
        Changother(Second, true, timers, ()=> { Time.timeScale = 0; });

    }


    public void ShowEsc()
    {
        ChangeBg(true);
        IsEsc = true;
        audioSource.Play();
        AudioManager.Instance.PauseMusic();
        Changother(Second, false, timers);
        Changother(Esc, true, timers,() => { Time.timeScale = 0; });
    }

    public void ShowPoetry()
    {
        ChangeBg(true);
        IsEsc = true;
        isSecond = true;
        audioSource.Play();
        AudioManager.Instance.PauseMusic();
        Esc.gameObject.SetActive(false);
        Changother(Esc, false, timers);
        Changother(Second, false, timers);
        Changother(Poetry, true, timers, () => { Time.timeScale = 0; });
    }

    public void BackEsc()
    {
        ChangeBg(false);
        IsEsc = false;
        audioSource.Pause();
        AudioManager.Instance.PlayMusic();
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
        Time.timeScale = 1;
        Changother(Esc, false, timers);
        Changother(Second, false, timers);
    }


}
