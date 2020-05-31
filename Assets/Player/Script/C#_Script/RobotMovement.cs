using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR     :         Balbona , Curie
// CREATE DATE     :    ????
// PURPOSE     :        Movement of the robot with the joystick
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================
public class RobotMovement : MonoBehaviour
{
    [SerializeField] float m_baseMoveSpeed = 5.0f;
    [SerializeField] float m_drag = 0.5f;
    [SerializeField] float m_terminalRotationSpeed = 25.0f;

    public VirtualJoystick m_moveJoystickScript = null;

    private Rigidbody controller;

    private float m_movementSpeed;

    public int m_thisEntityNumber;
    
    [HideInInspector] public Vector3 m_dir;

    [SerializeField]
    private Transform m_transform_to_rotate;
    [SerializeField]
    private Transform transform_LookAt;

    [SerializeField] private Animator m_Robot_Anim;
    
    private bool Is_this_Walking = false;
    [HideInInspector] public bool Is_this_atk = false;

   // [HideInInspector] public bool Is_push = false;

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
        {
            Robot_Mouvement();
        } 
    }

    private void Robot_Rotation()
    {
        m_transform_to_rotate.LookAt(transform_LookAt);
    }

    private void Update()
    {

        if (m_Robot_Anim != null)
        {   
            if ( gameObject.tag == "DestructionRobot" )
                m_Robot_Anim.SetBool("Is_Fire", Is_this_atk);
            //m_Robot_Anim.SetBool("Is_Pushing", Is_push);
            m_Robot_Anim.SetBool("Is_Walking", Is_this_Walking);
            

        }
    }

    private void RobotAnim()
    {
        if (m_dir.normalized != Vector3.zero)
        {
            
            if (m_Robot_Anim != null)
            {
                Is_this_Walking = true;
                
            }
        }
        else
        {
            
            if (m_Robot_Anim != null)
            {
                Is_this_Walking = false;
                SoundManager.Instance.m_robotMotor_Sound.Stop();
            }
        }
    }


    private void Robot_Mouvement()
    {
        m_dir = Vector3.zero;
        Vector3 dir_rot = Vector3.zero;

        // if the joystick position isn't null, it give a direciton to the player
        if (m_moveJoystickScript.m_InputDirection != Vector3.zero)
        {
            m_dir = new Vector3(m_moveJoystickScript.m_InputDirection.x, 0, m_moveJoystickScript.m_InputDirection.z).normalized;

            dir_rot = new Vector3(0, 0 , 0 );
        }
        
        // Stop the movement if the actual robot isn't this one
        if (GameManager.Instance.m_actualSelectedRobotNumber.Value != m_thisEntityNumber)
            m_dir = new Vector3(0, 0, 0);

        if (GameManager.Instance.m_actualSelectedRobotNumber.Value == m_thisEntityNumber)
        {
            

            if(SoundManager.Instance.m_robotMotor_Sound.isPlaying != true)
            {
                SoundManager.Instance.m_robotMotor_Sound.Play();
            }

            // Adpat the movement type because it did weird things with the flying robot
            if (gameObject.tag == "FlyingRobot")
            {
                RobotAnim();

                controller.AddForce(m_dir * m_movementSpeed, ForceMode.Impulse);
                Robot_Rotation();
                
            }
            else
            {
                transform.Translate(m_dir * m_movementSpeed);
                Robot_Rotation();
                RobotAnim();
            }         
        }


    }

}
