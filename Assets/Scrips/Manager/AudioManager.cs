using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//audioSource.Pause();//暂停
//audioSource.Play();//播放
//audioSource.Stop();//停止

public class AudioManager : MonoBehaviour
{
    public float AudioNum;
    public float Audionvalue;
    public static AudioManager Instance;
    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    private bool IsColseMusic = false;

    public void Awake()
    {
        //多场景继承该脚本
        if (audioSource == null)
        {
            audioSource = transform.GetComponent<AudioSource>();
            audioSource.loop = true;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 用于播放音效
    /// </summary>
    /// <param name="clip"></param>
    public void PlayEFAudio(AudioClip clip)
    {
        if (IsColseMusic)
        {
            return;
        }
        //从对象池获取一个音效播放器
        EFAudio ef = GameObject.Instantiate(GameManager.Instance.gameConf.EFAudio).GetComponent<EFAudio>();
        // //初始化音效播放器,导入要播放的音效片段
        ef.Init(clip);
    }
    //停止音乐
    public void StopMusic()
    {
        audioSource.Stop();
        IsColseMusic = true;
    }
    //暂停音乐
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    //开启音乐
    public void PlayMusic()
    {
        audioSource.Play();
        IsColseMusic = false;
    }

}
