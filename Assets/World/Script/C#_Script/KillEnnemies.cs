using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnnemies : MonoBehaviour
{
    [SerializeField] ParticleSystem m_smoke;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillEntitiesZone")
        {
            m_smoke.Play();
            Destroy(other.transform.parent.gameObject);
        }
    }
}
