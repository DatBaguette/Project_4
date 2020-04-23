using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovingCloud : MonoBehaviour
{
    private Animator m_animator;

    private void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    public void Move()
    {
        m_animator.Play("Move");
    }
}
