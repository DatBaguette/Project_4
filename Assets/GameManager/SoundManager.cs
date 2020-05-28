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
    
    
    public AudioSource m_robotFlamme;
    public AudioSource m_selectRobot;   
    public AudioSource m_coreSound;
    public AudioSource m_bossSound;
    public AudioSource m_robotCreat;
    public AudioSource m_robotMotor_Sound;
    public AudioSource m_robotDestruction;
    public AudioSource m_boomerangSoundWHOOSH;

    public List<AudioSource> m_soundeffect;
    public List<AudioSource> m_music;

    public Slider m_musicSlider;
    public Slider m_soundEffectSlider;

    [SerializeField] SavedCheckPoint m_saveData;

    public void Start()
    {
        m_soundeffect.Add(m_robotFlamme);
        m_soundeffect.Add(m_selectRobot);
        m_soundeffect.Add(m_coreSound);
        m_soundeffect.Add(m_bossSound);
        m_soundeffect.Add(m_robotCreat);
        m_soundeffect.Add(m_robotMotor_Sound);
        m_soundeffect.Add(m_robotDestruction);
        m_soundeffect.Add(m_boomerangSoundWHOOSH);
        m_soundeffect.Add(m_failSound);
        m_soundeffect.Add(m_succeedSound);

        if ( m_musicSlider != null && m_soundEffectSlider != null)
        {
            changeMusicVolume();
            changeSoundEffectVolume();
        }

        if (m_saveData.m_actualSceneID == 0)
            m_music[m_saveData.m_actualSceneID].Play();
        if (m_saveData.m_actualSceneID == 6)
            m_music[0].Play();
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

            foreach (AudioSource i in m_soundeffect)
            {
                i.volume = 0.1f;
            }
            
        }
        else
        {
            foreach (AudioSource i in m_soundeffect)
            {
                i.volume = 0f;
            }
            
        }
    }
}
