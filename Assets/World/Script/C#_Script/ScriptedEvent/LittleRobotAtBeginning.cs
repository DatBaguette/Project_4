using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobotAtBeginning : MonoBehaviour
{
    [SerializeField] Manage_Robot m_manageRobotScript;

    private void Start()
    {
        m_manageRobotScript.InstantiateRobot(1);
    }
}
