using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{

    public enum m_PlayerState
    {
        move_player,
        move_drone,
        boomerang,
        cinematic,
        Idle,
        Menu
    }

    public m_PlayerState m_currentPlayerState;
    
    public int m_actualSelectedRobotNumber = 0;

    public int m_robotNumber = 0;

    private void Start()
    {
        m_currentPlayerState = m_PlayerState.move_player;
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
        }
        if (Input.GetKeyDown("a"))
        {
            m_currentPlayerState = m_PlayerState.move_player;
        }
        if (Input.GetKeyDown("e"))
        {
            m_currentPlayerState = m_PlayerState.move_drone;
        }
    }

    public Vector3 RetrievePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            return (hit.point);
        }

        return (hit.point);
    }
}
