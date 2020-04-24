using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BridgeBehavior : MonoBehaviour
{
    [SerializeField] List<ButtonTrigger> m_button;

    private int m_canOpen = 2;

    private bool m_openOnce = false;

    private Animator m_animator;

    [SerializeField] GameObject m_invisibleWall;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();

        m_canOpen = m_button.Count;
    }

    private void Update()
    {
        if ( GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player )
            m_invisibleWall.SetActive(false);
        else
            m_invisibleWall.SetActive(true);

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
        else
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
    }

    IEnumerator BakeNavMeshSurface()
    {
        yield return new WaitForSeconds(8);

        m_invisibleWall.SetActive(false);
    }
}
