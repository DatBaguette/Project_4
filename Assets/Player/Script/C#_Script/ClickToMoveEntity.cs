using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMoveEntity : MonoBehaviour
{
    
    private NavMeshAgent m_navMeshAgent;

    [SerializeField] GameObject m_boomerangManager;
    [SerializeField] GameObject m_Joystick;

    private Vector3 m_baseJoystickPosition;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_baseJoystickPosition = m_Joystick.transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player 
            && GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.boomerang )
        {
            m_boomerangManager.SetActive(false);
            m_Joystick.transform.position = m_baseJoystickPosition;
        }
        else
        {
            m_boomerangManager.SetActive(true);
            m_Joystick.transform.position = m_baseJoystickPosition - new Vector3(1000, 0, 0);
        }

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
