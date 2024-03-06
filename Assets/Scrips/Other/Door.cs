using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    private bool Isfirst = true;//标志位是否第一次进入门
    public bool poetry;
    /// <summary>
    /// 玩家第一次进入门,隐藏玩家,实例化出运动曲线移动到第二关的位置
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player" && poetry)
        {
            NamePlane.Instance.ShowPoetry();//去往诗歌片段
        }

        if (other.tag == "Player" && Isfirst) 
        {
            Isfirst = false;
            EnvManager.Instance.Board.isTrigger = false;//开启绿地的碰撞箱
            other.GetComponent<Player>().RevampPlayer(false,false,true,0.5f);
            transform.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.5f).OnComplete(() =>
            {
                Trail trail = GameObject.Instantiate(GameManager.Instance.gameConf.SecondRondTail, transform.position, Quaternion.identity).GetComponent<Trail>();
                switch (GameManager.Instance.Guanka)
                {
                    case Guanka.YGYS:
                        trail.Init(GameManager.Instance.gameConf.YgysTransform, 5, ToPath);
                        break;
                    case Guanka.YMZZ:
                        trail.Init(GameManager.Instance.gameConf.YmzzTransform, 5, ToPath);
                        break;
                    case Guanka.FJQZ:
                        trail.Init(GameManager.Instance.gameConf.FjqzTransform, 5, ToPath);
                        break;
                    case Guanka.CMLX:
                        trail.Init(GameManager.Instance.gameConf.CmlxTransform, 5, ToPath);
                        break;
                }
            });
            //出现线条，隐藏们，玩家，场景出现
        }
    }

    /// <summary>
    /// 在第二关的位置当玩家离开门以后开始书写汉字
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraManager.Instance.ChangSecond();
            Destroy(this,0.5f);
            this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.5f);
            BhManager.Instance.ChangeHanzi(Hanzi.Start);//开启笔画书写汉字
        }
    }

    /// <summary>
    /// 在移动到第二部分后执行不同的玩家移动方法(不同的关卡第二部分不在相同位置)
    /// </summary>
    private void ToPath()
    {

        switch (GameManager.Instance.Guanka)
        {
            case Guanka.YGYS:
                GameManager.Instance.Player.transform.position = GameManager.Instance.gameConf.YGYSFSecondDoorVector;
                transform.position = GameManager.Instance.gameConf.YGYSFSecondDoorVector;
                break;
            case Guanka.YMZZ:
                GameManager.Instance.Player.transform.position = GameManager.Instance.gameConf.YMZZSecondDoorVector;
                transform.position = GameManager.Instance.gameConf.YMZZSecondDoorVector;
                break;
            case Guanka.FJQZ:
                GameManager.Instance.Player.transform.position = GameManager.Instance.gameConf.FJQZSecondDoorVector;
                transform.position = GameManager.Instance.gameConf.FJQZSecondDoorVector;
                break;
            case Guanka.CMLX:
                GameManager.Instance.Player.transform.position = GameManager.Instance.gameConf.CMLXSecondDoorVector;
                transform.position = GameManager.Instance.gameConf.CMLXSecondDoorVector;
                break;
        }
        
        GameManager.Instance.Player.RevampPlayer(true,true,true,0.5f);
        transform.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);
        GameManager.Instance.Player.IsCross = false;
    }
}