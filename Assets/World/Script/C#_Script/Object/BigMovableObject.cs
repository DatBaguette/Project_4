using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Behavior of the object that can be move by the plateform robot
/// 
/// </summary>

public class BigMovableObject : MonoBehaviour
{
    private Rigidbody m_rb;

    [SerializeField] GameObject m_parent;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.gameObject.tag == "PlateformRobot")
        {
            RobotInisialisation m_fRobotScript = other.GetComponent<RobotInisialisation>();

            Debug.Log(" robot scrip : " + m_fRobotScript);

            if ( m_fRobotScript != null)
            {
                if (m_fRobotScript.m_size == 2)
                    move();
            }
        }
    }

    private void move()
    {
        m_parent.gameObject.transform.Translate(transform.forward * 5 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillEntitiesZone")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
