using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Activate the button and "push" it if something touch it
/// 
/// </summary>

public class ButtonTrigger : MonoBehaviour
{
    /// <summary>
    /// Allow the associate object to know if this button is activate
    /// </summary>
    public bool m_activate = false;

    [SerializeField] GameObject m_associateHelper;

    private void OnTriggerEnter(Collider other)
    {
        if ( ( other.GetComponent<RobotMovement>() || other.GetComponent<ClickToMoveEntity>() ) && !m_activate)
        {
            m_activate = true;
            gameObject.transform.position += transform.forward * .3f;
            if (m_associateHelper != null)
                m_associateHelper.SetActive(true);
        }
    }
}
