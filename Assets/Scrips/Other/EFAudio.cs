using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��Ч���
public class EFAudio : MonoBehaviour
{
    private AudioSource audioSource;
    /// <summary>
    /// ����һ����ʼ��������
    /// </summary>
    /// <param name="clip"></param>
    public void Init(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.Instance.Audionvalue;
        audioSource.PlayOneShot(clip);
    }
    /// <summary>
    /// �������ɾ��
    /// </summary>
    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            Destroy(this.gameObject);   
        }
    }
}
