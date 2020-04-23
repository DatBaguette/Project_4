using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMoveEntity : MonoBehaviour
{
    
    private NavMeshAgent m_navMeshAgent;

    [SerializeField] GameObject m_boomerangManager;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player)
            m_boomerangManager.SetActive(false);

        if (Input.GetMouseButtonDown(0) && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player)
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
