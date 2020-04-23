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

    public LayerMask defautMask;
    public LayerMask boomerang;

    public m_PlayerState m_currentPlayerState;

    public int m_actualSelectedRobotNumber = 0;

    public int m_robotNumber = 0;

    public int m_actualRessources = 0;

    public bool m_boomerangLaunch = false;

    public bool[] m_robotCore = {false, false, false};

    public bool[] m_sizeUnlocked = { false, false};

    public StoryStep m_actualStoryStep = StoryStep.Intro;

    private void Start()
    {
        if ( m_actualStoryStep == StoryStep.Intro)
            m_currentPlayerState = m_PlayerState.move_drone;
        else
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

        if (Physics.Raycast(ray, out hit, 100 , currentLayer))
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
        if (!m_boomerangLaunch)
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
                m_currentPlayerState = m_PlayerState.move_drone;
            }
        }
       
    }


    public void RobotDestruction( int type, int size, GameObject ressources, GameObject robotObject)
    {
        Instantiate(ressources, robotObject.transform);

        RessourcesBehavior ressourcesScript = ressources.GetComponent<RessourcesBehavior>();

        // Value to change later
        ressourcesScript.m_ressourcesAmount = 100;
    }

    public void ActivateMagnet()
    {
        if ( m_currentPlayerState == m_PlayerState.boomerang)
        {
            m_currentPlayerState = m_PlayerState.move_player;
        }
        else
        {
            m_currentPlayerState = m_PlayerState.boomerang;
        }
    }

    public enum StoryStep
    {
        Intro,
        LevelOne,
        BridgePass,
    }

}
