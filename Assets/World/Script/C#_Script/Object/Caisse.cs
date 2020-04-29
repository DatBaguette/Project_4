using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caisse : MonoBehaviour, IFireReact
{

    void IFireReact.OnFire()
    {
        Debug.Log("ça brule !!!!");

        Destroy(gameObject);
    }

    void IFireReact.OnKillFire()
    {
        Debug.Log("Boom");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
