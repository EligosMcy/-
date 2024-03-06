using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeting : MonoBehaviour
{
    private Slider slider;//滑动条
    private Button Exit;//退出按钮
    private Image LittleBack1,LittleBack2;//小选择鼠标框
    private Button withoutChooice1, withoutChooice2;//选择鼠标按钮

    private void Awake()
    {
        //初始化获取
        withoutChooice1 = transform.Find("withoutChooice1").GetComponent<Button>();
        withoutChooice2 = transform.Find("withoutChooice2").GetComponent<Button>();
        LittleBack1 = transform.Find("withoutChooice1/LittleBack1").GetComponent<Image>();
        LittleBack2 = transform.Find("withoutChooice2/LittleBack2").GetComponent<Image>();
        Exit = transform.Find("Exit").GetComponent<Button>();
        slider = transform.Find("Music/Slider").GetComponent<Slider>();
        LittleBack1.gameObject.SetActive(true);
        LittleBack2.gameObject.SetActive(false);
    }

    private void Start()
    {
        //初始化功能
        withoutChooice1.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            LittleBack1.gameObject.SetActive(true);
            LittleBack2.gameObject.SetActive(false);
            //修改鼠标样式
            Cursor.SetCursor(GameManager.Instance.gameConf.Hand01,Vector2.zero, CursorMode.Auto);

        });

        withoutChooice2.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.ButtonClip);
            LittleBack2.gameObject.SetActive(true);
            LittleBack1.gameObject.SetActive(false);
            Cursor.SetCursor(GameManager.Instance.gameConf.Hand02, Vector2.zero, CursorMode.Auto);
            //修改鼠标样式
        });

        Exit.onClick.AddListener((() =>
        {
            //关闭设置界面
            transform.gameObject.SetActive(false);
        }));
        //初始化滑动条
        slider.value = AudioManager.Instance.AudioSource.volume;

        //添加滑动条功能
        slider.onValueChanged.AddListener((temp) =>
            {
                AudioManager.Instance.AudioSource.volume = AudioManager.Instance.AudioNum * temp;
                AudioManager.Instance.AudioSource.volume = temp;
                AudioManager.Instance.Audionvalue = temp;
            }
        );
    }

    
}
