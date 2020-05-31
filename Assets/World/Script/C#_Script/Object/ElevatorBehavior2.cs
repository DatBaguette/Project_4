using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehavior2 : MonoBehaviour
{
    /// <summary>
    /// the animator to trigger
    /// </summary>
    private Animator m_animator;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ClickToMoveEntity>())
        {
            
            m_animator.Play("MoveDown");
        }
    }
}
