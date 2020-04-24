using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Movement of the robot with the joystick
/// 
/// </summary>

public class RobotMovement : MonoBehaviour
{
    [SerializeField] float m_baseMoveSpeed = 5.0f;
    [SerializeField] float m_drag = 0.5f;
    [SerializeField] float m_terminalRotationSpeed = 25.0f;

    private VirtualJoystick m_moveJoystickScript;
    private GameObject[] m_moveJoystick = { null };

    private Rigidbody controller;

    private float m_movementSpeed;

    public int m_thisEntityNumber;

    void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = m_terminalRotationSpeed;
        controller.drag = m_drag;

        m_movementSpeed = m_baseMoveSpeed;

        m_moveJoystick = GameObject.FindGameObjectsWithTag("Joystick");
        m_moveJoystickScript = m_moveJoystick[0].GetComponent<VirtualJoystick>();
    }

    void Update()
    {

        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        // if the joystick position isn't null, it give a direciton to the player
        if (m_moveJoystickScript.m_InputDirection != Vector3.zero)
        {
            dir = new Vector3(m_moveJoystickScript.m_InputDirection.x, 0, m_moveJoystickScript.m_InputDirection.z);
        }

        // Stop the movement if the actual robot isn't this one
        if (GameManager.Instance.m_actualSelectedRobotNumber != m_thisEntityNumber)
            dir = new Vector3(0, 0, 0);

        // Adpat the movement type because it did weird things with the flying robot
        if ( gameObject.tag == "FlyingRobot")
            controller.AddForce(dir * m_movementSpeed, ForceMode.Impulse);
        else 
            transform.Translate(dir * m_movementSpeed);
    }
}
