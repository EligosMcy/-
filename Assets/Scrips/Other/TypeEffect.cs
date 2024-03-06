using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class TypeEffect : MonoBehaviour
{
    private float charsPerSecond = 0.1f;//打字时间间隔
    private TextMesh textMesh;//获取当前的TextMesh
    private string words;//需要保存的文字
    public int flagNum;//当前应该显示的是哪个放置盒

    private void Awake()
    {
        charsPerSecond = Mathf.Max(0, charsPerSecond);//防止打字间隔小于0
        textMesh = GetComponent<TextMesh>();//获取TextMesh脚本
        words = textMesh.text;//将初始文字赋值到words中
        textMesh.text = "";//清空显示的文字
    }
    /// <summary>
    /// 开启显示文字的协程
    /// </summary>
    /// <param name="LineNum"></param>段落的行数
    /// <param name="StartNum"></param>段落的起始行
    /// <param name="LineType"></param>段落行的类型
    public float StartEffect()
    {
        StartCoroutine(OnWhrite());
        return words.Length * charsPerSecond;
    }

    /// <summary>
    /// 显示文字的协程
    /// </summary>
    /// <param name="charsPerSecond"></param>每个字显示间隔
    /// <param name="LineNum"></param>当前段落的行数
    /// <param name="StartNum"></param>起始行的数字
    /// <param name="LineType"></param>行的类型
    /// <returns></returns>
    public IEnumerator OnWhrite()
    {
        yield return 0;
        int currentPos = 0;//当前打字位置
        while (currentPos < words.Length)
        {
            yield return new WaitForSeconds(charsPerSecond);
            string str = words.Substring(currentPos, 1);
            if (!str.Equals(" "))
            {
                currentPos++;
                textMesh.text = words.Substring(0, currentPos);
            }
            else
            {
                currentPos++;
            }

            if (str.Equals("\t"))//当遇到tab时就进行放置盒的显示
            {
                if (GameManager.Instance.Guanka == Guanka.YGYS)
                {
                    if (flagNum != 0 && flagNum != 4)
                    {
                        PickBoxManager.Instance.ShowFlags(flagNum);
                    }

                    if (flagNum == 4)
                    {
                        PickBoxManager.Instance.ShowPick(flagNum);//第四个是一起显示的
                        break;
                    }
                }
                else
                {
                    if (flagNum != 0)
                    {
                        PickBoxManager.Instance.ShowFlags(flagNum);
                    }
                }
            }
        }
        yield return 0;
        textMesh.text = words;
        if (GameManager.Instance.Guanka == Guanka.YGYS)
        {
            yield return new WaitForSeconds(1);
            if (flagNum == 4)
            {
                PickBoxManager.Instance.ShowFlags(flagNum);
            }
        }
        
    }
}
