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

    public VirtualJoystick m_moveJoystickScript = null;

    private Rigidbody controller;

    private float m_movementSpeed;

    public int m_thisEntityNumber;

    [SerializeField]
    private Transform m_transform_to_rotate;
    [SerializeField]
    private Transform transform_LookAt;

    private float f_ImpulseX;
    private float f_ImpulseZ;
    private Vector3 Vt3_Dir;

    void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = m_terminalRotationSpeed;
        controller.drag = m_drag;

        m_movementSpeed = m_baseMoveSpeed;
    }

    void FixedUpdate()
    {
        if (m_moveJoystickScript != null)
            Robot_Mouvement();
       
    }


    private void Robot_Rotation()
    {
        m_transform_to_rotate.LookAt(transform_LookAt);
    }


    private void Robot_Mouvement()
    {

        Vector3 dir = Vector3.zero;
        Vector3 dir_rot = Vector3.zero;


        // if the joystick position isn't null, it give a direciton to the player
        if (m_moveJoystickScript.m_InputDirection != Vector3.zero)
        {
            dir = new Vector3(m_moveJoystickScript.m_InputDirection.x, 0, m_moveJoystickScript.m_InputDirection.z).normalized;

            dir_rot = new Vector3(0, 0 , 0 );
        }
        
        // Stop the movement if the actual robot isn't this one
        if (GameManager.Instance.m_actualSelectedRobotNumber.Value != m_thisEntityNumber)
            dir = new Vector3(0, 0, 0);

        if (GameManager.Instance.m_actualSelectedRobotNumber.Value == m_thisEntityNumber)
        {
            // Adpat the movement type because it did weird things with the flying robot
            if (gameObject.tag == "FlyingRobot")
            {
                controller.AddForce(dir * m_movementSpeed, ForceMode.Impulse);
                Robot_Rotation();
            }
            else
            {
                transform.Translate(dir * m_movementSpeed);
                Robot_Rotation();
            }
                
        }

    }

}
