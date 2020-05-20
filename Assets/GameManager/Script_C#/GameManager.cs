using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
        Dead,
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
    public GD2Lib.IntVar m_actualRessources;

    /// <summary>
    /// Check if the player has unlocked robots cores
    /// </summary>
    public bool[] m_robotCore = { false, false, false };

    /// <summary>
    /// Check if the player has unlocked robots size
    /// </summary>
    public bool m_sizeUnlocked = false;


    public bool m_musicOn;
    public bool m_soundEffectOn;
    public Language m_actualLanguage;

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
    public int ActualCheckPointNumber
    {
        get
        {
            return m_actualCheckPointNumber;
        }
        set
        {
            m_nbRessourcesSinceLastCheckpoint = 0;

            m_actualCheckPointNumber = value;
        }
    }

    /// <summary>
    /// GameObject of the actual CheckPoint
    /// </summary>
    public List<GameObject> m_actualCheckPointObject;

    public List<GameObject> m_robots;
    public List<GameObject> m_robotsUI;

    public Manage_Robot m_manageRobotScript;

    public CinemachineVirtualCamera m_camera;

    public GameObject m_magnetActivateImage;

    private bool m_initDone = false;

    public TutoManager m_tutorialScript;

    public Dropdown m_language;

    public int m_nbRessourcesSinceLastCheckpoint;

    [SerializeField] GameObject m_ressourcesPrefab;


    private void Start()
    {

        m_nbRessourcesSinceLastCheckpoint = 0;

        m_initDone = true;

        if ( m_actualStoryStep == StoryStep.Intro)
            m_currentPlayerState = m_PlayerState.move_drone;
        else
            m_currentPlayerState = m_PlayerState.move_player;
    }

    private void Update()
    {
        StateController();

        if (m_initDone)
        {
            InitialiseSaveData();
            m_initDone = false;

            if ( m_saveData.m_actualSceneID != 1 )
                SoundManager.Instance.m_music[m_saveData.m_actualSceneID].Play();
        }
    }

    public void InitialiseSaveData()
    {
        ActualCheckPointNumber = m_saveData.m_checkPointNumberS;
        m_actualStoryStep = m_saveData.m_actualStoryStepS;
        m_actualRessources.Value = m_saveData.m_actualRessourcesS;
        m_robotCore = m_saveData.m_robotCoreS;
        m_sizeUnlocked = m_saveData.m_sizeUnlockedS;
        if ( m_tutorialScript != null )
            m_tutorialScript.m_actualTutoState = m_saveData.m_actualTutoStepS;
        m_musicOn = m_saveData.m_musicOn;
        m_soundEffectOn = m_saveData.m_soundEffectOn;
        m_actualLanguage = m_saveData.m_actualLanguage;

        m_player.GetComponent<NavMeshAgent>().enabled = false;

        m_player.transform.position = m_actualCheckPointObject[ActualCheckPointNumber].transform.position;

        m_player.GetComponent<NavMeshAgent>().enabled = true;

        switch (m_actualLanguage)
        {
            case Language.French:

                m_language.options.RemoveAt(1);

                break;

            case Language.English:

                m_language.options.RemoveAt(2);

                break;

        }
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

            //isBoomerang_out = true;

            m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_out = false;

        }
        else
        {
            m_player.GetComponentInChildren<SC_Boomerang>().InitBoom();
            m_magnetActivateImage.SetActive(true);
            m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_out = true;
        }

        m_navmesh.ResetPath();
    }

    /// <summary>
    /// If the player die
    /// </summary>
    /// 
    //public void playerDeath()
    //{
    //    m_actualRessources.Value -= m_nbRessourcesSinceLastCheckpoint;

    //    if (m_actualStoryStep == StoryStep.LevelOne)
    //        KillAllRobot();

    //    SaveData();

    //    SceneManager.LoadScene(m_saveData.m_actualSceneID);
    //}

    public void playerDeath()
    {
        m_currentPlayerState = m_PlayerState.Dead;
        m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isDead = true;
        m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isWalkingPressed = false;
        m_camera.Follow = m_player.transform;
        m_camera.LookAt = Instance.m_player.transform;
        StartCoroutine(RechargerLeNiveau(2.5f));
    }

    private IEnumerator RechargerLeNiveau(float WaitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitTime);

            m_actualRessources.Value -= m_nbRessourcesSinceLastCheckpoint;

            if (m_actualStoryStep == StoryStep.LevelOne)
                KillAllRobot();

            SaveData();

            SceneManager.LoadScene(m_saveData.m_actualSceneID);
        }
    }

    public void SaveData()
    {
        m_saveData.m_checkPointNumberS = ActualCheckPointNumber;
        m_saveData.m_actualStoryStepS = m_actualStoryStep;
        m_saveData.m_actualRessourcesS = m_actualRessources.Value;
        m_saveData.m_robotCoreS = m_robotCore;
        m_saveData.m_sizeUnlockedS = m_sizeUnlocked;
        if (m_tutorialScript != null)
            m_saveData.m_actualTutoStepS = m_tutorialScript.m_actualTutoState;
        m_saveData.m_musicOn = m_musicOn;
        m_saveData.m_soundEffectOn = m_soundEffectOn;
        m_saveData.m_actualLanguage = m_actualLanguage;
    }

    public void ChangeLanguage()
    {
        switch (m_language.options[m_language.value].text )
        {
            case "French" :

                m_actualLanguage = Language.French;

                break;

            case "English":

                m_actualLanguage = Language.English;

                break;
        }

        playerDeath();
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

        MenuManager.Instance.m_ressourcesUi.SetActive(true);

        m_camera.Follow = m_player.transform;
        m_camera.LookAt = Instance.m_player.transform;

        m_currentPlayerState = m_PlayerState.move_player;

        RobotInisialisation m_robotScript = m_robots[i].GetComponent<RobotInisialisation>();

        switch (m_robotScript.m_robotType)
        {
            case Robot_Type.Flying:

                switch (m_robotScript.m_size)
                {
                    case 1: m_actualRessources.Value += m_manageRobotScript.price[0]; break;

                    case 2: m_actualRessources.Value += m_manageRobotScript.price[1]; break;
                }

                break;

            case Robot_Type.Platforme:

                switch (CraftManager.Instance.m_choosenSize)
                {
                    case 1: m_actualRessources.Value += m_manageRobotScript.price[2]; break;

                    case 2: m_actualRessources.Value += m_manageRobotScript.price[3]; break;
                }

                break;

            case Robot_Type.Destruction:

                switch (CraftManager.Instance.m_choosenSize)
                {
                    case 1: m_actualRessources.Value += m_manageRobotScript.price[4]; break;

                    case 2: m_actualRessources.Value += m_manageRobotScript.price[5]; break;
                }

                break;
        }
        
        GameObject ressourcesCreate = Instantiate(m_ressourcesPrefab, m_robots[i].transform);
        ressourcesCreate.transform.SetParent(gameObject.transform);
        ressourcesCreate.transform.position += new Vector3(0, 2, 0);
        RessourcesBehavior ressourcesScript = ressourcesCreate.GetComponent<RessourcesBehavior>();
        ressourcesScript.m_ressourcesAmount = 0;
        ressourcesScript.m_harvested = true;

        Destroy(m_robots[i]);
        Destroy(m_robotsUI[i]);
    }

    public enum StoryStep
    {
        Intro,
        LevelOne,
    }

    public enum Language
    {
        French,
        English
    }

}
