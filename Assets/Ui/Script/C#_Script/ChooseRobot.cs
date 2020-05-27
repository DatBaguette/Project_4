using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

/// <summary>
/// 
/// It will allow player to switch between a robot and the main character
/// 
/// </summary>

public class ChooseRobot : MonoBehaviour
{
    public int m_uiRobotNumber;
    
    public GameObject m_associateRobot;
    
    public GameObject m_player;

    private CinemachineVirtualCamera m_playerCamera;

    private Image m_img;

    public BIlly_Anim_CTRL m_Player_Animator;

    private GameObject m_tablette;

    [SerializeField] GameObject m_selectedFeedback;

    private void Start()
    {
        GameObject[]  m_playerCameraObject = GameObject.FindGameObjectsWithTag("PlayerCamera");

        m_playerCamera = m_playerCameraObject[0].GetComponent<CinemachineVirtualCamera>();

        m_img = gameObject.GetComponent<Image>();

        m_tablette = m_player.GetComponent<ClickToMoveEntity>().m_tablette;

    }

    public void SelectRobot()
    {
        if (GameManager.Instance.m_actualSelectedRobotNumber.Value == m_uiRobotNumber)
        {
            GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_player;

            GameManager.Instance.m_actualSelectedRobotNumber.Value = 0;

            m_playerCamera.Follow = m_player.transform;
            m_playerCamera.LookAt = m_player.transform;

            MenuManager.Instance.m_magnetLogo.SetActive(true);
            MenuManager.Instance.m_openCraftLogo.SetActive(true);
            
            MenuManager.Instance.m_ressourcesUi.SetActive(true);

            m_selectedFeedback.SetActive(false);

            if (m_Player_Animator != null)
            {
                m_Player_Animator.isTabletteOpen = false;
            }

            m_tablette.SetActive(false);

        }
        else
        {
            GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_drone;

            GameManager.Instance.m_actualSelectedRobotNumber.Value = m_uiRobotNumber;

            m_playerCamera.Follow = m_associateRobot.transform;
            m_playerCamera.LookAt = m_associateRobot.transform;

            SoundManager.Instance.m_selectRobot.Play();

            for ( int i=0; i<GameManager.Instance.m_robotsUI.Count; i++)
            {
                if (GameManager.Instance.m_robotsUI[i] != null)
                    GameManager.Instance.m_robotsUI[i].GetComponent<ChooseRobot>().m_selectedFeedback.SetActive(false);
            }

            m_selectedFeedback.SetActive(true);

            MenuManager.Instance.m_menuAnimator.Play("Hidding");
            MenuManager.Instance.m_menuOpen = false;
            MenuManager.Instance.m_magnetLogo.SetActive(true);
            MenuManager.Instance.m_Joystick.transform.position = MenuManager.Instance.m_baseJoystickPosition;

            MenuManager.Instance.m_magnetLogo.SetActive(false);
            MenuManager.Instance.m_openCraftLogo.SetActive(false);
            
            MenuManager.Instance.m_ressourcesUi.SetActive(false);

            if (m_Player_Animator != null)
            {
                m_Player_Animator.isTabletteOpen = true;
            }

            m_tablette.SetActive(true);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }
}
