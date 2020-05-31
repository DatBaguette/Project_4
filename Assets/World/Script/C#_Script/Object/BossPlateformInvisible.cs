using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hide the plateform when you controle a robot
/// </summary>

public class BossPlateformInvisible : MonoBehaviour
{
    [SerializeField] GameObject m_objectToHide;

    private void Update()
    {
        if ( GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player )
            m_objectToHide.SetActive(false);
        else
            m_objectToHide.SetActive(true);
    }
}
