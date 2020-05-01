using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int m_thisCheckPointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ClickToMoveEntity>() )
            GameManager.Instance.m_actualCheckPointNumber = m_thisCheckPointNumber;
    }
}
