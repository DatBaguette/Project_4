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

    private void Start()
    {
        GameObject[]  m_playerCameraObject = GameObject.FindGameObjectsWithTag("PlayerCamera");

        m_playerCamera = m_playerCameraObject[0].GetComponent<CinemachineVirtualCamera>();

        m_img = gameObject.GetComponent<Image>();

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

            m_img.color = Color.white;

        }
        else
        {
            GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_drone;

            GameManager.Instance.m_actualSelectedRobotNumber.Value = m_uiRobotNumber;

            m_playerCamera.Follow = m_associateRobot.transform;
            m_playerCamera.LookAt = m_associateRobot.transform;

            for ( int i=0; i<GameManager.Instance.m_robotsUI.Count; i++)
            {
                GameManager.Instance.m_robotsUI[i].GetComponent<Image>().color = Color.white;
            }

            m_img.color = Color.green;

            MenuManager.Instance.m_craftMenu.SetActive(false);
            MenuManager.Instance.m_magnetLogo.SetActive(false);
            MenuManager.Instance.m_openCraftLogo.SetActive(false);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }
}
