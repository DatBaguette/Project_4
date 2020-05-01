﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderBehavior : MonoBehaviour
{
    [SerializeField] GameObject m_objectToGrind;

    [SerializeField] GameObject m_elevator;

    [SerializeField] int m_numberOfGrind = 1;

    private bool m_canGrind = true;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "MovableObject")
        {
            Destroy(other.gameObject);
            if ( m_canGrind)
            {
                m_canGrind = false;
                StartCoroutine(GrindObject());
            }
        }
    }

    IEnumerator GrindObject()
    {
        yield return new WaitForSeconds(.5f);

        m_canGrind = true;

        m_numberOfGrind -= 1;
        m_objectToGrind.transform.position -= new Vector3(0, 4, 0);

        if ( m_numberOfGrind == 0)
        {
            m_elevator.GetComponent<Animator>().Play("MoveUp");
        }
    }
}
