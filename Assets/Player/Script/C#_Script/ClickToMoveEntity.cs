﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveEntity : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;

    [SerializeField] public int m_thisEntityNumber;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_thisEntityNumber == GameManager.Instance.m_actualSelectedRobotNumber 
            && (GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player || GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_drone))
        {
            m_navMeshAgent.destination = GameManager.Instance.RetrievePosition();
        }
    }
}
