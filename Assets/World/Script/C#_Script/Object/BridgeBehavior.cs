using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BridgeBehavior : MonoBehaviour
{
    [SerializeField] List<ButtonTrigger> m_button;

    private int m_canOpen = 2;

    private Animator m_animator;

    [SerializeField] NavMeshSurface m_surface;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if ( m_canOpen > 0)
        {
            for (int i = 0; i < m_button.Count; i++)
            {
                if (m_button[i].m_activate)
                    m_canOpen -= 2;
            }
        }
        else
        {
            m_animator.Play("Open");
            StartCoroutine(BakeNavMeshSurface());
        }
    }

    IEnumerator BakeNavMeshSurface()
    {
        yield return new WaitForSeconds(10);

        m_surface.BuildNavMesh();
    }
}
