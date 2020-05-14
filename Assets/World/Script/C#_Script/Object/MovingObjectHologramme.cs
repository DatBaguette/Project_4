using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectHologramme : MonoBehaviour
{
    [SerializeField] GameObject m_hologramme;

    void Update()
    {
        if ( GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_drone)
        {
            m_hologramme.SetActive(true);
        }
        else
        {
            m_hologramme.SetActive(false);
        }
    }
}
