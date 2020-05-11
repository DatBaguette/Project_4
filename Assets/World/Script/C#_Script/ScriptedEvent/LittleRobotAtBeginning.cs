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
    [SerializeField] GameObject m_craftMenuOpenIcon;
    [SerializeField] GameObject m_boomerangIcon;
    [SerializeField] GameObject m_ressourcesUi;

    [SerializeField] CinemachineVirtualCamera m_playerCamera;

    [SerializeField] GameObject m_player;

    [SerializeField] GameObject m_flyingRobotCore;

    [SerializeField] GameObject m_movingCloud;

    private void Start()
    {
        GameManager.Instance.m_actualSelectedRobotNumber.Value = 1;

        GameManager.Instance.m_robotNumber += 1;

        var robot = Instantiate(m_robotPrefab, m_spawnPosition.transform);

        robot.gameObject.name = "RobotIntro";

        RobotMovement robotScriptToMove = robot.gameObject.GetComponent<RobotMovement>();
        robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

        m_playerCamera.Follow = robot.transform;
        m_playerCamera.LookAt = robot.transform;

        m_movingCloud.GetComponent<IntroMovingCloud>().Move();

    }

    private void Update()
    {
        ChangeEventState();
    }

    public void ChangeEventState()
    {
        if ( GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro )
        {

            m_selectRobotUi.SetActive(false);
            m_craftMenuOpenIcon.SetActive(false);
            m_boomerangIcon.SetActive(false);
            MenuManager.Instance.m_craftMenu.SetActive(false);
            m_ressourcesUi.SetActive(false);
        }
        else
        {
            m_selectRobotUi.SetActive(true);
            m_craftMenuOpenIcon.SetActive(true);
            m_boomerangIcon.SetActive(true);
            MenuManager.Instance.m_craftMenu.SetActive(true);
            m_ressourcesUi.SetActive(true);

            m_playerCamera.Follow = m_player.transform;
            m_playerCamera.LookAt = m_player.transform;

            GameManager.Instance.m_robotNumber -= 1;
            GameManager.Instance.m_actualSelectedRobotNumber.Value = 0;

            m_flyingRobotCore.SetActive(true);

            GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_player;

            Destroy(gameObject);
        }
    }
}
