using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Behavior of the object that can be move by the plateform robot
/// 
/// </summary>

public class MovableObject : MonoBehaviour
{
    private Rigidbody m_rb;

    [SerializeField] GameObject m_parent;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.gameObject.tag == "FlyingRobot")
        {
            RobotInisialisation m_fRobotScript = other.GetComponent<RobotInisialisation>();

            if (m_fRobotScript != null)
            {
                if (m_fRobotScript.m_size == 2)
                    move();
            }
        }
        if (other.gameObject.tag == "PlateformRobot")
        {
           
            move();
        }
    }

    private void move()
    {
        m_parent.gameObject.transform.Translate(transform.forward * 5 * Time.deltaTime, Space.World);
    }
}
