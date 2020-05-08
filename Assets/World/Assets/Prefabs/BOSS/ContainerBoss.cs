using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Child;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = m_Child.transform.position;
    }
}
