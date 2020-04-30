using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Check if the pressure plate is activate
/// 
/// </summary>

public class PressurePlate : MonoBehaviour
{
    public bool m_activate = false;

    private void OnTriggerEnter(Collider other)
    {

        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            m_activate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            m_activate = false;
        }
    }
}
