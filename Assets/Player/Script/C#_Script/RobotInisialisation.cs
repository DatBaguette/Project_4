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
    /// Robot size
    /// </summary>
    public int m_size;

    private Rigidbody controller;

    /// <summary>
    /// FlameThrower area of attack
    /// </summary>
    [SerializeField] ParticleSystem m_fire;

    /// <summary>
    /// Cone collider to burn things
    /// </summary>
    [SerializeField] GameObject m_flameCollisonTracker;
    
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

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case Robot_Type.Destruction:

                gameObject.transform.position += new Vector3(0, 3, 0);

                m_fire.Stop(true);

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

                if ( ( Input.touchCount == 2 || Input.GetKeyDown(KeyCode.T) ) && 
                    GameManager.Instance.m_actualSelectedRobotNumber.Value == m_movementScript.m_thisEntityNumber)
                {
                    m_fire.Play(true);
                    m_flameCollisonTracker.SetActive(true);

                    StartCoroutine(StopFlame());

                }

                break;
        }
    }

    IEnumerator StopFlame()
    {
        yield return new WaitForSeconds(2);

        m_fire.Stop(true);
        m_flameCollisonTracker.SetActive(false);
    }
}
