using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{

    public enum m_PlayerState
    {
        move_player,
        boomerang,
        mouve_drone1,
        mouve_drone2,
        mouve_drone3,
        cinématique,
        Idle,
        Menu
    }

    public LayerMask defautMask;
    public LayerMask boomerang;

    public m_PlayerState m_currentPlayerState;


    private void Start()
    {
        m_currentPlayerState = m_PlayerState.move_player;
    }

    public Vector3 RetrievePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        LayerMask currentLayer;

        if(m_currentPlayerState == m_PlayerState.boomerang)
        {
            currentLayer = boomerang;
        }
        else
        {
            currentLayer = defautMask;
        }

        if (Physics.Raycast(ray, out hit, 100 ))
        {
            return (hit.point);
        }

        return (hit.point);
    }



    private void Update()
    {
        StateController();
    }

    public void StateController()
    {
        if (Input.GetKeyDown("z"))
        {
            m_currentPlayerState = m_PlayerState.boomerang;
            Debug.Log(m_currentPlayerState);
        }
        if (Input.GetKeyDown("a"))
        {
            m_currentPlayerState = m_PlayerState.move_player;
            Debug.Log(m_currentPlayerState);
        }
        if (Input.GetKeyDown("e"))
        {
            m_currentPlayerState = m_PlayerState.mouve_drone1;
            Debug.Log(m_currentPlayerState);
        }
        if (Input.GetKeyDown("e"))
        {
            m_currentPlayerState = m_PlayerState.mouve_drone2;
            Debug.Log(m_currentPlayerState);
        }

    }
   
}
