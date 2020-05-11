using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnnemies : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillEntitiesZone")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
