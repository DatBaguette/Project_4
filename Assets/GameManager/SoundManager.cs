using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Save all the sounds
/// </summary>

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_failSound;
    public AudioSource m_succeedSound;

    public List<AudioSource> m_music;

    public Slider m_musicSlider;
    public Slider m_soundEffectSlider;

    [SerializeField] SavedCheckPoint m_saveData;

    public void Start()
    {
        changeMusicVolume();
        changeSoundEffectVolume();

        if (m_saveData.m_actualSceneID == 0 || m_saveData.m_actualSceneID == 5)
            m_music[m_saveData.m_actualSceneID].Play();
    }

    public void changeMusicVolume()
    {
        if (m_musicSlider.value == 0)
            GameManager.Instance.m_musicOn = false;
        else
            GameManager.Instance.m_musicOn = true;

        if (GameManager.Instance.m_musicOn)
        {
            for ( int i = 0; i<m_music.Count; i++)
            {
                m_music[i].volume = .1f;
            }
        }
        else
        {
            for (int i = 0; i < m_music.Count; i++)
            {
                m_music[i].volume = 0;
            }
        }
    }

    public void changeSoundEffectVolume()
    {
        if (m_soundEffectSlider.value == 0)
            GameManager.Instance.m_soundEffectOn = false;
        else
            GameManager.Instance.m_soundEffectOn = true;

        if ( GameManager.Instance.m_soundEffectOn)
        {
            m_failSound.volume = .1f;
            m_succeedSound.volume = .1f;
        }
        else
        {
            m_failSound.volume = 0;
            m_succeedSound.volume = 0;
        }
    }
}
