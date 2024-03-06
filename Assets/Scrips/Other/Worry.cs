using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 错误效果脚本
/// </summary>
public class Worry : MonoBehaviour
{
    public void Init(UnityAction Action = null)
    {
        gameObject.transform.DOPunchPosition(new Vector3(0, 0.2f, 0), 0.4f, 0, 0.5f);//自身和放置盒向 上小跳一下
        gameObject.transform.DOPunchRotation(new Vector3(0, 0, -0.15f), 0.4f, 0, 0.5f).OnComplete((() =>
        {
            if (Action != null)
            {
                Action();
            }
            Destroy(this.gameObject);
        }));
    }
    
}
