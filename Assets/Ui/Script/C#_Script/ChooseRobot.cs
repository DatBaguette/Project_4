using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChooseRobot : MonoBehaviour
{
    public int m_uiRobotNumber;

    public GameObject m_associateRobot;
    public GameObject m_player;

    private CinemachineVirtualCamera m_playerCamera;

    private void Start()
    {
        GameObject[]  m_playerCameraObject = GameObject.FindGameObjectsWithTag("PlayerCamera");

        m_playerCamera = m_playerCameraObject[0].GetComponent<CinemachineVirtualCamera>();
    }

    public void SelectRobot()
    {
        if (GameManager.Instance.m_actualSelectedRobotNumber == m_uiRobotNumber)
        {
            GameManager.Instance.m_actualSelectedRobotNumber = 0;

            m_playerCamera.Follow = m_player.transform;
            m_playerCamera.LookAt = m_player.transform;
        }
        else
        {
            GameManager.Instance.m_actualSelectedRobotNumber = m_uiRobotNumber;

            m_playerCamera.Follow = m_associateRobot.transform;
            m_playerCamera.LookAt = m_associateRobot.transform;
        }
    }
}
