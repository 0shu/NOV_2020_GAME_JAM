using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static AudioManager s_Instance = null;

    private void Awake()
    {
        //Jank but cba to do it a good way
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_Instance != this)
            DestroyImmediate(gameObject);
    }

    [SerializeField]
    private AudioSource m_MusicSource;
    [SerializeField]
    private AudioSource m_SFXSource;

    [SerializeField]
    private AudioClip m_MenuLoop, m_InGameLoop;
    [SerializeField]
    private AudioClip m_Buzz, m_Checkpoint, m_Magnet, m_Splash;

    public enum MusicClip
    {
        MenuLoop, InGameLoop
    }
    public enum SFXClip
    { 
        Buzz, Checkpoint, Magnet, Splash
    }

    public static void PlayMusic(MusicClip clip)
    {
        switch (clip)
        {
            case MusicClip.MenuLoop:
                {
                    s_Instance.m_MusicSource.clip = s_Instance.m_MenuLoop;
                    break;
                }
            case MusicClip.InGameLoop:
                {
                    s_Instance.m_MusicSource.clip = s_Instance.m_InGameLoop;
                    break;
                }
            default:
                break;
        }

        s_Instance.m_MusicSource.Play();
    }

    public static void PlaySFX(SFXClip clip) //add volume parameter for attenuation
    {
        switch (clip)
        {
            case SFXClip.Buzz:
                {
                    s_Instance.m_MusicSource.PlayOneShot(s_Instance.m_Buzz);
                    break;
                }
            case SFXClip.Checkpoint:
                {
                    s_Instance.m_MusicSource.PlayOneShot(s_Instance.m_Checkpoint);
                    break;
                }
            case SFXClip.Magnet:
                {
                    s_Instance.m_MusicSource.PlayOneShot(s_Instance.m_Magnet);
                    break;
                }
            case SFXClip.Splash:
                {
                    s_Instance.m_MusicSource.PlayOneShot(s_Instance.m_Splash);
                    break;
                }
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}