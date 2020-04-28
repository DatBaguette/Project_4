using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovingCloud : MonoBehaviour
{
    [SerializeField] Animator m_animator;

    public void Move()
    {
        m_animator.Play("Move");
    }
}
