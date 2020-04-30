using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<DestructiblesObjects>() )
        {
            IFireReact fireReact = other.GetComponent<DestructiblesObjects>();

            fireReact.OnFire();

        }
    }
}
