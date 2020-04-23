using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if ( other.gameObject.tag == "PlateformRobot")
        {
            m_parent.gameObject.transform.Translate(transform.forward * Time.deltaTime, Space.World);
        }
    }
}
