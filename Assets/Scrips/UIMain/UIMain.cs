using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public Button BeginBtn, ExitBtn, SetupBtn,BackBtn1, ExitBtn1;
    public GameObject BG1;
    public  GameObject BG2;

    public Button Begin1,Begin2,Begin3,Begin4;
    public Button SetingButton;
    public GameObject SetBg;
    private void Start()
    {
        BG2.SetActive(false);
        SetBg.SetActive(false);
        BeginBtn.onClick.AddListener(() =>
        {
            BG2.SetActive(true);
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
        });
        ExitBtn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        });
        Begin1.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            UIManager.Instance.ToScence(2);
        });
        Begin2.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            UIManager.Instance.ToScence(3);
        });
        Begin3.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            UIManager.Instance.ToScence(4);
        });
        Begin4.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            UIManager.Instance.ToScence(5);
        });
        ExitBtn1.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        });
        BackBtn1.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            BG2.SetActive(false);
        });

        SetingButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            SetBg.SetActive(true);
        });
    }
  
}
