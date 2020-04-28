using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int m_thisCheckPointNumber;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.m_actualCheckPointObject = gameObject;
        GameManager.Instance.m_actualCheckPointNumber = m_thisCheckPointNumber;
    }
}
