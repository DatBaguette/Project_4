using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMoveEntity : MonoBehaviour
{
    
    private NavMeshAgent m_navMeshAgent;

    [HideInInspector] public int m_thisEntityNumber;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_thisEntityNumber == GameManager.Instance.m_actualSelectedRobotNumber 
            && (GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player || GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_drone))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if ( Physics.Raycast( ray, out hitInfo, Mathf.Infinity))
                m_navMeshAgent.destination = GameManager.Instance.RetrievePosition();
        }
    }
}
