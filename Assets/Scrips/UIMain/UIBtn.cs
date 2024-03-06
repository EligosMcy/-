using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UIBtn : MonoBehaviour
{

    private GameObject Begin1,BG1;
    private void Start()
    {
        Begin1 = transform.GetChild(0).gameObject;
        BG1 = transform.GetChild(1).gameObject;
    }
    public void OnMouseEnter()
    {
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.gameConf.SwitchClip);
        Begin1.SetActive(true);
        BG1.SetActive(true);
    }

    public void OnMouseExit()
    {
        Begin1.SetActive(false);
        BG1.SetActive(false);
    }
    
}
