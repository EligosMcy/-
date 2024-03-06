using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//音效组件
public class EFAudio : MonoBehaviour
{
    private AudioSource audioSource;
    /// <summary>
    /// 给与一个初始化的声音
    /// </summary>
    /// <param name="clip"></param>
    public void Init(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.Instance.Audionvalue;
        audioSource.PlayOneShot(clip);
    }
    /// <summary>
    /// 播放完毕删除
    /// </summary>
    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            Destroy(this.gameObject);   
        }
    }
}
