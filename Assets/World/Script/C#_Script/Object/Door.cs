using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]
    private GameObject m_himSelf;

    [SerializeField] List<PressurePlate> m_theScript;

    private Animator m_animator;

    private bool m_activate = false;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        int checkPressurePlate = 0;

        for (int i = 0; i < m_theScript.Count; i++)
        {
            if (m_theScript[i].m_getIsOpen)
                checkPressurePlate += 1;
                
        }

        if (checkPressurePlate == m_theScript.Count && !m_activate)
        {
            m_animator.Play("Open");
            m_activate = true;

            if (!SoundManager.Instance.m_succeedSound.isPlaying)
                SoundManager.Instance.m_succeedSound.Play();
        }

        if (checkPressurePlate != m_theScript.Count && m_activate)
        {
            if ( m_activate )
            {
                m_animator.Play("Close");

                m_activate = false;

                if (!SoundManager.Instance.m_failSound.isPlaying)
                    SoundManager.Instance.m_failSound.Play();
            }
        }

        checkPressurePlate = 0;
    }
}
