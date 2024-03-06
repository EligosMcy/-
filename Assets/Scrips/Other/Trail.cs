using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Trail : MonoBehaviour
{
    private Vector3 FirstVector3;

    /// <summary>
    /// 第二关初始化路径方法
    /// </summary>
    /// <param name="RunPath"></param>路径的数组
    /// <param name="time"></param>运动消耗的事件
    /// <param name="resolution"></param>//曲线的弯曲程度
    /// <param name="ToPath"></param>//在完成路径的行走以后要进行的方法
    public void Init(Transform[] RunPath, int time, UnityAction ToPath)
    {
        //这句话就是吧Transform类型的数组转化为vector3类型数组
        var positions = RunPath.Select(u => u.position).ToArray();
        CameraManager.Instance.ChangeShow(true, this.transform);
        transform.DOPath(positions, time, PathType.CatmullRom, PathMode.Full3D, 10, Color.blue)
            .SetLookAt(0).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(this.gameObject);
                if (ToPath != null)
                {
                    CameraManager.Instance.ChangeShow(false);
                    ToPath();
                }
            });
    }

    /// <summary>
    /// 不同的关卡场景使用到的路径初始化方法
    /// </summary>
    /// <param name="guanka"></param>当前是哪个关卡
    /// <param name="PlanNum"></param>当前是哪个场景生成
    public void Init(int PlanNum)
    {
        GetFirstVector(PlanNum);
        CameraManager.Instance.ChangeShow(true, this.transform);//修改摄像机的状态
        transform.DOMove(FirstVector3, 1).SetEase(Ease.InFlash).OnComplete((
            () =>
            {
                Instantiate(GameManager.Instance.gameConf.Boom, FirstVector3, Quaternion.identity);
                ShowLine(PlanNum);
            }));
    }

    private void GetFirstVector(int PlanNum)
    {
        switch (GameManager.Instance.Guanka)
        {
            case Guanka.YGYS:
                FirstVector3 = GameManager.Instance.gameConf.YGYSFirstVector3s[PlanNum - 2];
                break;
            case Guanka.YMZZ:
                FirstVector3 = GameManager.Instance.gameConf.YMZZFirstVector3s[PlanNum - 2];
                break;
            case Guanka.FJQZ:
                FirstVector3 = GameManager.Instance.gameConf.FJQZFirstVector3s[PlanNum - 2];
                break;
            case Guanka.CMLX:
                FirstVector3 = GameManager.Instance.gameConf.CMLXFirstVector3s[PlanNum - 2];
                break;
        
        }
    }

    /// <summary>
    /// 展示生成的景物，并且开始下一个阶段
    /// </summary>
    /// <param name="PlanNum"></param>
    private void ShowLine(int PlanNum)
    {
        transform.DOMoveZ(1, 1).OnComplete(//当路径移动到位置的时候暂停1秒
            () =>
            {
                Destroy(this.gameObject);
                CameraManager.Instance.ChangeShow(false);
                GameManager.Instance.Player.RevampPlayer(true, true);
                //玩家的取消控制放在了PickFlag上
            });
        GameManager.Instance.OpenPlan(PlanNum);//开启下一阶段
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Second")
        {
            other.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);
        }

        if (other.tag == "Mizi")
        {
            SpriteRenderer[] sprite = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in sprite)
            {
                spriteRenderer.DOColor(Color.white, 0.5f);
            }
        }
    }
}
