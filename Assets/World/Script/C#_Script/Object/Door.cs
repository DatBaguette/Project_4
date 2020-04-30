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
    [SerializeField] List<PressurePlate> m_theScriptP;

    /// <summary>
    /// List of button's script
    /// </summary>
    [SerializeField] List<ButtonTrigger> m_theScriptB;

    private Animator m_animator;

    /// <summary>
    ///  Check if the object has been activated
    /// </summary>
    private bool m_activate = false;

    /// <summary>
    /// Check if an entity must stay on the button or pressure plate to open the door 
    /// </summary>
    [SerializeField] bool m_onePushMode = false;

    /// <summary>
    /// Number of button OR pressure plate
    /// </summary>
    private int m_numberOfTrigger;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();

        if (m_onePushMode)
        {
            if (m_theScriptB.Count <= 0)
                Debug.Log("Changer de mode !");

            m_numberOfTrigger = m_theScriptB.Count;
        }
        else
        {
            if (m_theScriptP.Count <= 0)
                Debug.Log("Changer de mode !");

            m_numberOfTrigger = m_theScriptP.Count;
        }
    }

    private void Update()
    {
        int checkPressurePlate = 0;

        // Check if associate pressure plate are activated and increase a variable
        for (int i = 0; i < m_numberOfTrigger; i++)
        {
            if (m_onePushMode)
            {
                if (m_theScriptB[i].m_activate)
                    checkPressurePlate += 1;
            }
            else
            {
                if (m_theScriptP[i].m_activate)
                    checkPressurePlate += 1;
            }


        }

        //Open the door if all pressure plate are activate
        if (checkPressurePlate == m_numberOfTrigger && !m_activate)
        {
            
            m_animator.Play("Open");
            m_activate = true;

            if (!SoundManager.Instance.m_succeedSound.isPlaying)
                SoundManager.Instance.m_succeedSound.Play();
        }

        //Close the door if one pressure plate is desactivate
        if (checkPressurePlate != m_numberOfTrigger && m_activate)
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
