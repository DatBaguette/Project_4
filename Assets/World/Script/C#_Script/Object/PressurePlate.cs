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

    [SerializeField] GameObject m_associateHelper;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            m_activate = true;
            if ( m_associateHelper != null )
                m_associateHelper.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            m_activate = false;
            if (m_associateHelper != null)
                m_associateHelper.SetActive(false);
        }
    }
}
