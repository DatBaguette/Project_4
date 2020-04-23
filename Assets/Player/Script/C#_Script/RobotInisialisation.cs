using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// It will assign parameter to robot depending on their type
/// 
/// </summary>

public class RobotInisialisation : MonoBehaviour
{
    public Robot_Type m_robotType;

    private Rigidbody controller;

    private float m_timer = 0;

    [SerializeField] GameObject m_cone;

    private RobotMovement m_movementScript;

    private void Start()
    {

        controller = GetComponent<Rigidbody>();

        m_movementScript = gameObject.GetComponent<RobotMovement>();

    }

    
    // Behavior depending on the robot type
    private void Update()
    {
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                break;

            case Robot_Type.Platforme:

                //Nothing for the moment

                break;

            case Robot_Type.Destruction:

                if (m_timer > 0)
                {
                    m_timer -= .1f;
                    m_cone.SetActive(true);
                }
                else
                {
                    m_cone.SetActive(false);
                }

                if (Input.touchCount == 2 && GameManager.Instance.m_actualSelectedRobotNumber == m_movementScript.m_thisEntityNumber)
                {
                    m_timer = 2;

                }

                break;
        }
    }
}
