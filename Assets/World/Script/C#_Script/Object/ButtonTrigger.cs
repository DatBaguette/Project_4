using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public bool m_activate = false;


    private void OnTriggerEnter(Collider other)
    {
        if ( ( other.GetComponent<RobotMovement>() || other.GetComponent<ClickToMoveEntity>() ) && !m_activate)
        {
            m_activate = true;
            gameObject.transform.position += transform.forward * .3f;
        }
    }
}
