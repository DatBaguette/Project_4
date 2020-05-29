// ===============================
// AUTHOR     :         Balbona , Curie
// CREATE DATE     :    ????
// PURPOSE     :        Manage the all game and transfère data between script
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================

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
    /// <summary>
    /// State of Nilly
    /// </summary>
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

    /// <summary>
    /// SO where the data are saved
    /// </summary>
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

    
    // There is some paramètre to interact with the ui and the game fx
    public bool m_musicOn;
    public bool m_soundEffectOn;
    public Language m_actualLanguage;

    /// <summary>
    /// This is billy himself
    /// </summary>
    public GameObject m_player;

    /// <summary>
    /// This is the navmesh
    /// </summary>
    public NavMeshAgent m_navmesh;

    //To layer that are us in game
    public LayerMask defautMask;
    public LayerMask boomerang;

    /// <summary>
    /// Current player state 
    /// </summary>
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

    /// <summary>
    /// ID of the actual checkpoint
    /// </summary>
    public int m_actualCheckPointNumber = 0;
    
    /// <summary>
    /// The number of checkpoint on the current lvl
    ///  It returne an int 
    /// </summary>
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

    /// <summary>
    /// All the robot are on this list
    /// </summary>
    public List<GameObject> m_robots;

    /// <summary>
    /// The container, that robots us in th UI
    /// </summary>
    public List<GameObject> m_robotsUI;

    /// <summary>
    /// Creat and manage robots
    /// </summary>
    public Manage_Robot m_manageRobotScript;

    /// <summary>
    /// The used camera
    /// </summary>
    public CinemachineVirtualCamera m_camera;

    /// <summary>
    /// Boomerang Buton 
    /// </summary>
    public GameObject m_magnetActivateImage;

    /// <summary>
    /// check if the initialisation is finished
    /// </summary>
    private bool m_initDone = false;

    /// <summary>
    /// The script of the tutorial 
    /// </summary>
    public TutoManager m_tutorialScript;

    /// <summary>
    /// the current langue use 
    /// </summary>
    public Dropdown m_language;

    /// <summary>
    /// actual ressources since last checkpoint
    /// </summary>
    public int m_nbRessourcesSinceLastCheckpoint;

    /// <summary>
    /// The prefab of the ressource
    /// </summary>
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
                SoundManager.Instance.m_music[m_saveData.m_actualSceneID-1].Play();
        }
    }

    /// <summary>
    /// Save all the data in the SO m_saveData
    /// </summary>
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

    /// <summary>
    /// Cheat keycode in editor
    /// </summary>
    public void StateController()
    {
#if UNITY_EDITOR
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
            if (Input.GetKeyDown("g"))
            {
                KillAllRobot();
            }
#endif 
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
    /// Intence player death
    /// </summary>
    public void playerDeath()
    {
        
        if ( m_saveData.m_actualSceneID == 1)
        {
            SceneManager.LoadScene(m_saveData.m_actualSceneID);
        }
        else
        {
            m_currentPlayerState = m_PlayerState.Dead;
            m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isDead = true;
            m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isWalkingPressed = false;

            m_camera.Follow = m_player.transform;
            m_camera.LookAt = Instance.m_player.transform;

            StartCoroutine(RechargerLeNiveau(2.5f));
        }
    }

    /// <summary>
    /// delay to remake the game 
    /// </summary>
    /// <param name="WaitTime"></param>
    /// <returns></returns>
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

    /// <summary>
    /// save data on the so
    /// </summary>
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


    /// <summary>
    /// change the text depending on the select language
    /// </summary>
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
            SoundManager.Instance.m_robotDestruction.Play();

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

        m_player.GetComponent<ClickToMoveEntity>().m_tablette.SetActive(false);

        BIlly_Anim_CTRL m_animationScript = m_player.GetComponent<ClickToMoveEntity>().m_Player_Animator;

        if (m_player.GetComponent<ClickToMoveEntity>().m_Player_Animator != null)
        {
            m_player.GetComponent<ClickToMoveEntity>().m_Player_Animator.isTabletteOpen = false;
        }

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

    /// <summary>
    /// The different story step
    /// </summary>
    public enum StoryStep
    {
        Intro,
        LevelOne,
    }

    /// <summary>
    /// The different language
    /// </summary>
    public enum Language
    {
        French,
        English
    }

}
