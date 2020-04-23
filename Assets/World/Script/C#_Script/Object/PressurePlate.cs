﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool m_getIsOpen = false;


    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            Debug.Log("banen");
            m_getIsOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() )
        {
            m_getIsOpen = false;
        }
    }
}
