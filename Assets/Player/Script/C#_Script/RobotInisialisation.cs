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
    /// <summary>
    /// Robot type to change their behavior
    /// </summary>
    public Robot_Type m_robotType;

    /// <summary>
    /// Robot size ( 1 /2 / 3 )
    /// </summary>
    public int m_size;

    private Rigidbody controller;

    /// <summary>
    /// Time of activation of the flameThrower
    /// </summary>
    private float m_timer = 0;

    /// <summary>
    /// FlameThrower area of attack
    /// </summary>
    [SerializeField] GameObject m_cone;
    
    /// <summary>
    /// Script that allow the robot to move
    /// </summary>
    private RobotMovement m_movementScript;

    private void Start()
    {

        controller = GetComponent<Rigidbody>();

        m_movementScript = gameObject.GetComponent<RobotMovement>();

        //Initialise the robot depending of his type
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case Robot_Type.Platforme:

                //Nothing for the moment

                break;

            case Robot_Type.Destruction:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;
        }

    }

    
    // Behavior depending on the robot type
    private void Update()
    {
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                //Nothing for the moment

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

                if ( ( Input.touchCount == 2 || Input.GetKeyDown(KeyCode.T) ) && 
                    GameManager.Instance.m_actualSelectedRobotNumber.Value == m_movementScript.m_thisEntityNumber)
                {
                    m_timer = 2;

                }

                break;
        }
    }
}
