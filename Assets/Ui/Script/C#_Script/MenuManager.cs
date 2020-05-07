using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// 
/// It will show the menu and allow to give money for prototyping
/// 
/// </summary>

public class MenuManager : Singleton<MenuManager>
{
    [Tooltip("craft menu Manager")]
    public GameObject m_craftMenu;

    [Tooltip("Icon to open craft menu")]
    public GameObject m_openCraftLogo;

    [Tooltip("Icon to activate magnet")]
    public GameObject m_magnetLogo;

    public bool m_menuOpen = false;

    public GameObject m_Joystick;

    public Animator m_menuAnimator;

    [Tooltip("Text gameObject that will show the actual amount of ressources")]
    [SerializeField] Text m_ressourcesText;

    [SerializeField] List<GameObject> m_CraftMenuRobotTypeArea;

    [SerializeField] GameObject m_sizeArea;

    /// <summary>
    /// Allow to reset the joystick position
    /// </summary>
    public Vector3 m_baseJoystickPosition;

    private void Start()
    {
        m_baseJoystickPosition = m_Joystick.transform.position;
    }

    public void AddRessources()
    {
        GameManager.Instance.m_actualRessources.Value += 100;

        GameManager.Instance.m_navmesh.ResetPath();
    }

    public void DeleteRessources()
    {
        if (GameManager.Instance.m_actualRessources.Value > 100)
        {
            GameManager.Instance.m_actualRessources.Value -= 100;
        }
        else
        {
            GameManager.Instance.m_actualRessources.Value = 0;
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }

    public void OpenCloseMenu()
    {
        if (m_menuOpen)
        {
            m_menuAnimator.Play("Hidding");
            m_menuOpen = false;
            m_magnetLogo.SetActive(true);
            m_Joystick.transform.position = m_baseJoystickPosition;
        }
        else
        {
            m_menuAnimator.Play("Open");
            m_menuOpen = true;
            m_magnetLogo.SetActive(false);
            m_Joystick.transform.position = m_baseJoystickPosition - new Vector3(1000, 0, 0);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }

    private void Update()
    {
        m_ressourcesText.text = GameManager.Instance.m_actualRessources.Value.ToString();

        // Desactivate areas in the craft menu if the player dont have the robot's core associate
        for ( int i = 0; i < m_CraftMenuRobotTypeArea.Count; i++)
        {
             if (!GameManager.Instance.m_robotCore[i])
                 m_CraftMenuRobotTypeArea[i].SetActive(false);

             else
                 m_CraftMenuRobotTypeArea[i].SetActive(true);
        }

        if (!GameManager.Instance.m_sizeUnlocked)
            m_sizeArea.SetActive(false);

        else
            m_sizeArea.SetActive(true);

        if (!m_menuOpen)
        {
            // desactivate the interface if the player dont control the character
            if (GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player
                && GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.boomerang)
            {
                m_magnetLogo.SetActive(false);
                m_Joystick.transform.position = m_baseJoystickPosition;
            }
            else
            {
                m_magnetLogo.SetActive(true);
                m_Joystick.transform.position = m_baseJoystickPosition - new Vector3(1000, 0, 0);
            }
        }
    }
}
