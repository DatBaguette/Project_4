using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Effect of fire on the object
/// 
/// Destroy him and activate environnement's events
/// </summary>

public class DestructiblesObjects : MonoBehaviour, IFireReact
{
    [SerializeField] List<ActivateEnvironnementAnimation> m_EAScript;

    [SerializeField] List<DestructiblesObjects> m_objectToKill;

    public bool m_dissolve = false;
    public float m_dissolveValue = 0;

    private Material m_material;

    private void Start()
    {
        m_material = gameObject.GetComponent<Renderer>().material;
    }

    void IFireReact.OnFire()
    {

        for ( int i=0; i<m_EAScript.Count; i++)
        {
            m_EAScript[i].Activate = true;
        }

        for (int i = 0; i < m_objectToKill.Count; i++)
        {
            m_objectToKill[i].m_dissolve = true;
        }

        m_dissolve = true;
    }

    void IFireReact.OnKillFire()
    {
        Debug.Log("Boom");
    }

    private void Update()
    {
        if ( m_dissolve)
        {
            m_dissolveValue += .01f;
            m_material.SetFloat("Vector1_601ED788", m_dissolveValue);
        }

        if (m_dissolveValue >= 1)
            Destroy(gameObject);
    }
}
