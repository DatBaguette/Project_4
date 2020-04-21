using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// It will Trigger at the beginning of the game when the player control a little flying robot
/// 
/// </summary>

public class LittleRobotAtBeginning : MonoBehaviour
{
    [SerializeField] Manage_Robot m_manageRobotScript;


    [SerializeField] GameObject m_fliyingbot;
    private void Start()
    {
        m_manageRobotScript.InstantiateRobot(m_fliyingbot);
    }
}
