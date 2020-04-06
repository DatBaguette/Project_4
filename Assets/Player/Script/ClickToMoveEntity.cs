using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveEntity : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player)
        {
            m_navMeshAgent.destination = GameManager.Instance.RetrievePosition();
        }
    }
}
