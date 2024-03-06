using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//audioSource.Pause();//��ͣ
//audioSource.Play();//����
//audioSource.Stop();//ֹͣ

public class AudioManager : MonoBehaviour
{
    public float AudioNum;
    public float Audionvalue;
    public static AudioManager Instance;
    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    private bool IsColseMusic = false;

    public void Awake()
    {
        //�ೡ���̳иýű�
        if (audioSource == null)
        {
            audioSource = transform.GetComponent<AudioSource>();
            audioSource.loop = true;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ���ڲ�����Ч
    /// </summary>
    /// <param name="clip"></param>
    public void PlayEFAudio(AudioClip clip)
    {
        if (IsColseMusic)
        {
            return;
        }
        //�Ӷ���ػ�ȡһ����Ч������
        EFAudio ef = GameObject.Instantiate(GameManager.Instance.gameConf.EFAudio).GetComponent<EFAudio>();
        // //��ʼ����Ч������,����Ҫ���ŵ���ЧƬ��
        ef.Init(clip);
    }
    //ֹͣ����
    public void StopMusic()
    {
        audioSource.Stop();
        IsColseMusic = true;
    }
    //��ͣ����
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    //��������
    public void PlayMusic()
    {
        audioSource.Play();
        IsColseMusic = false;
    }

}
