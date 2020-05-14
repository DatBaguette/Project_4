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

    public Slider m_musicSlider;
    public Slider m_soundEffectSlider;

    public void changeMusicVolume()
    {
        if (m_musicSlider.value == 0)
            GameManager.Instance.m_musicOn = false;
        else
            GameManager.Instance.m_musicOn = true;

        if (GameManager.Instance.m_musicOn)
        {
            // music volume .1f
        }
        else
        {
            // music volume 0
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
