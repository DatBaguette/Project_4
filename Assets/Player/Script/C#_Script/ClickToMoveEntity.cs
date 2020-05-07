using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// It will hide the interface if the character isn't controlled
/// and allow to move him
/// 
/// </summary>

public class ClickToMoveEntity : MonoBehaviour
{
    
    private NavMeshAgent m_navMeshAgent;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {

        // Move the player
        if (Imput_Manager.Instance.GetInput() == true && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player)
        {

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if ( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 5))
                m_navMeshAgent.destination = GameManager.Instance.RetrievePosition();
        }

        if (Input.GetKeyDown("k")){
            Debug.Log("oui");
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
        }
    }
}
