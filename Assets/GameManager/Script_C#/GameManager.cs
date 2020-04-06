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

    public m_PlayerState m_currentPlayerState;


    private void Start()
    {
        m_currentPlayerState = m_PlayerState.move_player;
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
            m_currentPlayerState = m_PlayerState.mouve_drone1;
        }
        if (Input.GetKeyDown("e"))
        {
            m_currentPlayerState = m_PlayerState.mouve_drone2;
        }
    }
}
