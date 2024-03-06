using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Effect : MonoBehaviour
{
    private Animator animator;
    public bool isMain;
    public Animator Animator => animator = GetComponent<Animator>();
    void Update()
    {

        if (isMain)
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75)
            {
                //换声音 切换场景
                SceneManager.LoadScene(1);
            }
           
        }
        else
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {

                Destroy(this.gameObject);
            }
        }
        
    }
}
