// ===============================
// AUTHOR     :         Balbona 
// CREATE DATE     :    ????
// PURPOSE     :        Trow flam LoL
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================
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

        if ( other.GetComponent<BigDestructiblesObjects>() )
        {
            IFireReact fireReact = other.GetComponent<BigDestructiblesObjects>();

            fireReact.OnFire();

        }

        if (other.GetComponent<BossBehaviour>())
        {
            IFireReact fireReact = other.GetComponent<BossBehaviour>();

            fireReact.OnFire();

        }
    }
}
