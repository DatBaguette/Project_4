using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// It will check if all pressure plate associate has been actiavate
/// and open or close the door depending of this state.
/// 
/// </summary>

public class Door : MonoBehaviour
{
    /// <summary>
    /// Store the parent of the object
    /// </summary>
    [SerializeField]
    private GameObject m_himSelf;

    /// <summary>
    /// List of pressure plate's script
    /// </summary>
    [SerializeField] List<PressurePlate> m_theScript;

    private Animator m_animator;

    /// <summary>
    ///  Check if the object has been activated
    /// </summary>
    private bool m_activate = false;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        int checkPressurePlate = 0;

        // Check if associate pressure plate are activated and increase a variable
        for (int i = 0; i < m_theScript.Count; i++)
        {
            if (m_theScript[i].m_getIsOpen)
                checkPressurePlate += 1;
                
        }

        //Open the door if all pressure plate are activate
        if (checkPressurePlate == m_theScript.Count && !m_activate)
        {
            m_animator.Play("Open");
            m_activate = true;

            if (!SoundManager.Instance.m_succeedSound.isPlaying)
                SoundManager.Instance.m_succeedSound.Play();
        }

        //Close the door if one pressure plate is desactivate
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
