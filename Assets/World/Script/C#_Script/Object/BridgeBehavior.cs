using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// Activate the bridge if all associated button are activate
/// 
/// </summary>

public class BridgeBehavior : MonoBehaviour
{
    // List of button script
    [SerializeField] List<ButtonTrigger> m_button;

    // Check if all button has been activate
    private int m_canOpen = 0;

    // Check if the bridge is open
    private bool m_openOnce = false;

    // Check if the invisible wall must be desactivated
    private bool m_invisibleWallDesactivated = false;

    private Animator m_animator;

    // Wall that block the player's path
    [SerializeField] GameObject m_invisibleWall;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();

        m_canOpen = m_button.Count;
    }

    private void Update()
    {
        // Desactivate the wall to let the flying robot pass
        if ( m_invisibleWall != null)
        {
            if (GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player)
                m_invisibleWall.SetActive(false);
            else
                m_invisibleWall.SetActive(true);
        }

        // Check if all buttons are activate
        if ( m_canOpen > 0)
        {
            for (int i = 0; i < m_button.Count; i++)
            {
                if (m_button[i].m_activate)
                    m_canOpen -= 1;
                else
                    m_canOpen = m_button.Count;
            }
        }
        else // Open the bridge
        {
            if (!m_openOnce)
            {
                m_animator.Play("Open");

                if (!SoundManager.Instance.m_succeedSound.isPlaying)
                    SoundManager.Instance.m_succeedSound.Play();

                StartCoroutine(BakeNavMeshSurface());

                m_openOnce = true;
            }
        }

        if (m_invisibleWallDesactivated)
        {
            m_invisibleWall.SetActive(false);
        }
    }

    /// <summary>
    /// Desactivate the invisible wall when the bridge is fully open
    /// </summary>
    IEnumerator BakeNavMeshSurface()
    {
        yield return new WaitForSeconds(5);

        m_invisibleWallDesactivated = true;
    }
}
