using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBox : MonoBehaviour
{
    private Vector3 StartPlayerVestor;

    void Start()
    {
        switch (GameManager.Instance.Guanka)
        {
            case Guanka.YGYS:
                StartPlayerVestor = GameManager.Instance.gameConf.YgysPlayerVector3;
                break;
            case Guanka.YMZZ:
                StartPlayerVestor = GameManager.Instance.gameConf.YmzzPlayerVector3;
                break;
            case Guanka.FJQZ:
                StartPlayerVestor = GameManager.Instance.gameConf.FjqzPlayerVector3;
                break;
            case Guanka.CMLX:
                StartPlayerVestor = GameManager.Instance.gameConf.CmlxPlayerVector3;
                break;
        }
    }
    public void OnCollisionEnter2D(Collision2D coll)
    {
        coll.transform.position = StartPlayerVestor;
    }
}
