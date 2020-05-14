using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnvironnementAnimation : MonoBehaviour
{
    [SerializeField] Animator m_animation;

    public bool m_activate = false;

    public bool Activate
    {
        get
        {
            return m_activate;
        }
        set
        {
            m_animation.Play("Fall");
            m_activate = value;
        }
    }
}
