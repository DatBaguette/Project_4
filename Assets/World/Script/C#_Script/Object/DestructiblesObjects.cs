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

    void IFireReact.OnFire()
    {
        Debug.Log("ça brule !!!!");

        for ( int i=0; i<m_EAScript.Count; i++)
        {
            m_EAScript[i].Activate = true;
        }

        Destroy(gameObject);
    }

    void IFireReact.OnKillFire()
    {
        Debug.Log("Boom");
    }
}
