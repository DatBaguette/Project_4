using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// 
/// It will Trigger at the beginning of the game when the player control a little flying robot
/// 
/// </summary>

public class LittleRobotAtBeginning : MonoBehaviour
{
    [SerializeField] Manage_Robot m_manageRobotScript;

    [SerializeField] GameObject m_spawnPosition;

    [SerializeField] GameObject m_robotPrefab;

    [SerializeField] GameObject m_selectRobotUi;
    [SerializeField] GameObject m_ressourcesUi;

    [SerializeField] CinemachineVirtualCamera m_playerCamera;

    [SerializeField] GameObject m_player;

    [SerializeField] VirtualJoystick m_joystickScript;

    private GameObject m_robot;

    private bool m_introActivated = false;

    private void Start()
    {
        Introduction();

    }

    private void Update()
    {
        ChangeEventState();
    }

    public void ChangeEventState()
    {
        if ( GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro )
        {
            if (!m_introActivated)
            {
                Introduction();
            }

            MenuManager.Instance.m_selectRobotUi.SetActive(false);
            MenuManager.Instance.m_openCraftLogo.SetActive(false);
            MenuManager.Instance.m_magnetLogo.SetActive(false);
            MenuManager.Instance.m_craftMenu.SetActive(false);
            MenuManager.Instance.m_ressourcesUi.SetActive(false);
        }
        else
        {
            MenuManager.Instance.m_selectRobotUi.SetActive(true);
            MenuManager.Instance.m_openCraftLogo.SetActive(true);
            MenuManager.Instance.m_magnetLogo.SetActive(true);
            MenuManager.Instance.m_craftMenu.SetActive(true);
            MenuManager.Instance.m_ressourcesUi.SetActive(true);

            m_playerCamera.Follow = m_player.transform;
            m_playerCamera.LookAt = m_player.transform;

            GameManager.Instance.m_robotNumber -= 1;
            GameManager.Instance.m_actualSelectedRobotNumber.Value = 0;

            GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_player;

            Destroy(gameObject);
        }
    }

    public void Introduction()
    {

        GameManager.Instance.m_actualSelectedRobotNumber.Value = 1;

        GameManager.Instance.m_robotNumber += 1;

        m_robot = Instantiate(m_robotPrefab, m_spawnPosition.transform);

        RobotMovement robotScript = m_robot.GetComponent<RobotMovement>();
        robotScript.m_moveJoystickScript = m_joystickScript;

        m_robot.gameObject.name = "RobotIntro";

        RobotMovement robotScriptToMove = m_robot.gameObject.GetComponent<RobotMovement>();
        robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

        m_playerCamera.Follow = m_robot.transform;
        m_playerCamera.LookAt = m_robot.transform;

        m_introActivated = true;
    }
}
