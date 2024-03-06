using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public void ToScence(int Num)
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        if (Num>5)
        {
            AudioManager.Instance.AudioSource.Stop();
        }
        else
        {
            AudioManager.Instance.AudioSource.Play();
        }
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
        SceneManager.LoadScene(Num);
    }

    public void Exit()
    {
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
        Application.Quit();
    }
}
