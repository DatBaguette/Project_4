using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Script that manage all the game
/// 
/// </summary>

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

    public SavedCheckPoint m_saveData;

    /// <summary>
    /// Used to trigger scripted event or scripted gameplay
    /// </summary>
    public StoryStep m_actualStoryStep = StoryStep.Intro;

    /// <summary>
    /// Number of ressources in the inventory
    /// </summary>
    [SerializeField]
    public GD2Lib.IntVar m_actualRessources;

    /// <summary>
    /// Check if the player has unlocked robots cores
    /// </summary>
    public bool[] m_robotCore = { false, false, false };

    /// <summary>
    /// Check if the player has unlocked robots size
    /// </summary>
    public bool[] m_sizeUnlocked = { false, false };

    public GameObject m_player;

    public NavMeshAgent m_navmesh;

    public LayerMask defautMask;
    public LayerMask boomerang;

    public m_PlayerState m_currentPlayerState;

    /// <summary>
    /// The ID of the robot that the player is currently controlling
    /// </summary>
    [SerializeField]
    public GD2Lib.IntVar m_actualSelectedRobotNumber;

    /// <summary>
    /// Number of robot in the game
    /// </summary>
    public int m_robotNumber = 0;

    //public bool m_boomerangLaunch = false;

    /// <summary>
    /// ID of the actual checkpoint
    /// </summary>
    public int m_actualCheckPointNumber = 0;

    /// <summary>
    /// GameObject of the actual CheckPoint
    /// </summary>
    public List<GameObject> m_actualCheckPointObject;

    public List<GameObject> m_robots;
    public List<GameObject> m_robotsUI;

    public Manage_Robot m_manageRobotScript;

    public CinemachineVirtualCamera m_camera;

    public GameObject m_magnetActivateImage;

    private void Start()
    {
        m_actualCheckPointNumber = m_saveData.m_checkPointNumberS;
        m_actualStoryStep = m_saveData.m_actualStoryStepS;
        m_actualRessources = m_saveData.m_actualRessourcesS;
        m_robotCore = m_saveData.m_robotCoreS;
        m_sizeUnlocked = m_saveData.m_sizeUnlockedS;

        m_player.GetComponent<NavMeshAgent>().enabled = false;

        m_player.transform.position = m_actualCheckPointObject[m_actualCheckPointNumber].transform.position;

        m_player.GetComponent<NavMeshAgent>().enabled = true;

        if ( m_actualStoryStep == StoryStep.Intro)
            m_currentPlayerState = m_PlayerState.move_drone;
        else
            m_currentPlayerState = m_PlayerState.move_player;
    }

    /// <summary>
    /// Retrieve the position of the mouse on the screen
    /// </summary>
    public Vector3 RetrievePosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        LayerMask currentLayer;

        if (m_currentPlayerState == m_PlayerState.boomerang)
        {
            currentLayer = boomerang;
        }
        else
        {
            currentLayer = defautMask;
        }

        if (Physics.Raycast(ray, out hit, 100, currentLayer))
        {

            return (hit.point);
        }

        return (hit.point);

    }

    private void Update()
    {
        StateController();
    }

    // Cheat keycode
    public void StateController()
    {
        //if (!m_boomerangLaunch)
        //{
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
            if (Input.GetKeyDown("g"))
            {
                KillAllRobot();
            }
        //}
       
    }

    /// <summary>
    /// Change the state of the player to activate the magnet
    /// </summary>
    public void ActivateMagnet()
    {
        if ( m_currentPlayerState == m_PlayerState.boomerang)
        {

            m_player.GetComponentInChildren<SC_Boomerang>().CurrentBoomerangstat = BoomerangState.Off;
            m_player.GetComponentInChildren<SC_Boomerang>().RestardBoomrangPos();
            m_player.GetComponentInChildren<SC_Boomerang>().Restart_boomerang();
            //m_player.GetComponentInChildren<SC_Boomerang>().m_Boomerang.position = m_player.transform.position;
            m_currentPlayerState = m_PlayerState.move_player;
            m_magnetActivateImage.SetActive(false);

        }
        else
        {
            m_player.GetComponentInChildren<SC_Boomerang>().InitBoom();
            m_magnetActivateImage.SetActive(true);
        }

        m_navmesh.ResetPath();
    }

    /// <summary>
    /// If the player die
    /// </summary>
    public void playerDeath()
    {
        SaveData();

        KillAllRobot();

        SceneManager.LoadScene(m_saveData.m_actualSceneID);

        //ResetAllEnnemies();
        //m_player.transform.position = m_actualCheckPointObject.transform.position;
        //KillAllRobot();

    }

    public void SaveData()
    {
        m_saveData.m_checkPointNumberS = m_actualCheckPointNumber;
        m_saveData.m_actualStoryStepS = m_actualStoryStep;
        m_saveData.m_actualRessourcesS = m_actualRessources;
        m_saveData.m_robotCoreS = m_robotCore;
        m_saveData.m_sizeUnlockedS = m_sizeUnlocked;
    }

    /// <summary>
    /// Reset all the ennemies
    /// </summary>
    public void ResetAllEnnemies()
    {
        GameObject[] m_ennemies = GameObject.FindGameObjectsWithTag("Ghost");

        for ( int i = 0; i < m_ennemies.Length; i++)
        {
            GhostBehavior ghostScript = m_ennemies[i].GetComponent<GhostBehavior>();
            ghostScript.ResetPosition();
        }
        
    }

    /// <summary>
    /// Kill all robots that the player can control
    /// </summary>
    public void KillAllRobot()
    {
        if ( m_robotNumber > 0)
        {
            for (int i = 0; i < m_robotNumber; i++)
            {
                KillOneRobot(i);
            }

            m_robotNumber = 0;
        }

        m_navmesh.ResetPath();
    }

    /// <summary>
    /// Kill a robot
    /// </summary>
    public void KillOneRobot(int i)
    {
        MenuManager.Instance.m_magnetLogo.SetActive(true);
        MenuManager.Instance.m_openCraftLogo.SetActive(true);

        m_camera.Follow = m_player.transform;
        m_camera.LookAt = Instance.m_player.transform;

        m_currentPlayerState = m_PlayerState.move_player;

        RobotInisialisation m_robotScript = m_robots[i].GetComponent<RobotInisialisation>();

        int type = 0;

        switch (m_robotScript.m_robotType)
        {
            case Robot_Type.Flying:
                type = 0;
                break;

            case Robot_Type.Platforme:
                type = 3;
                break;

            case Robot_Type.Destruction:
                type = 6;
                break;
        }

        m_actualRessources.Value += m_manageRobotScript.price[type];

        Destroy(m_robots[i]);
        Destroy(m_robotsUI[i]);
    }

    public enum StoryStep
    {
        Intro,
        LevelOne,
    }

}
