using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// 笔画方格脚本
/// </summary>
public class Stroke : MonoBehaviour
{
    private float ShowTiem = 0.75f;
    public int StrokeNum;//这个脚本当前的笔顺是第几位

    public Hanzi Hanzi;

    public Animator Animator;

    public Hanzi NextHanzi;

    public bool isLast;

    public int OffetNum;

    public void Init(int strokeNum,Hanzi hanzi,Animator animator,int offetnum, bool islast = false)
    {
        StrokeNum = strokeNum;
        Hanzi = hanzi;
        Animator = animator;
        OffetNum = offetnum;
        isLast = islast;
    }

    public string WriteOut(int NowNum)
    {
        if (NowNum == StrokeNum)
        {
            //对方块自己来说首先要跳动,慢慢移动消失.
            transform.DOPunchPosition(new Vector3(0, 0.5f, 0), 0.4f, 2, 0.5f);
            transform.DOPunchRotation(new Vector3(0, 0, 0.375f), 0.4f, 2, 0.5f).OnComplete((() =>
            {
                GetComponent<BoxCollider2D>().enabled = false;
                MeshRenderer[] MeshS = gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer M in MeshS)
                {
                    M.enabled = false;
                }

                Destroy(transform.gameObject, ShowTiem+0.25f);
                //传地回去我的位置
            }));
            //播放对应的笔画轨迹

            Animator.Play(Hanzi.ToString() + " " + StrokeNum);

            //显示对应的笔画
            if (isLast)
            {
                Instantiate(GameManager.Instance.gameConf.Great, transform.position + new Vector3(0, 0.1f, -0.1f), Quaternion.identity).GetComponent<Worry>().Init();
                Invoke("ChangeHanzi", ShowTiem);
                return "isLast";
                //当是最后一个死亡的时候还要再去告诉BhManger让HanziNumber = 0
                //传递回去自己的当前汉字  然后  BhManager自己判断 开启下一个汉字的生成
            }
            Instantiate(GameManager.Instance.gameConf.Great, transform.position + new Vector3(0, 0.1f, -0.1f), Quaternion.identity).GetComponent<Worry>().Init();
            Invoke("MadeOneStroke", ShowTiem);
            return "isOk";
        }
        else
        {
            transform.DOPunchScale(new Vector3(0.1f, 0, 0), 0.2f, 1, 0).OnComplete((() =>
            {
                Instantiate(GameManager.Instance.gameConf.Worrg, transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity).GetComponent<Worry>().Init();
            }));
            return "isNo";
        }
    }

    private void MadeOneStroke()
    {
        BhManager.Instance.MadeOneStroke(Hanzi, Animator, OffetNum);
    }

    private void ChangeHanzi()
    {
        BhManager.Instance.ChangeHanzi(Hanzi);
    }
}
