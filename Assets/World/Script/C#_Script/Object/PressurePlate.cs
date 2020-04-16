using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool GetIsOpen = false;

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ClickToMoveEntity>())
        {
            GetIsOpen = true;
        }
    }
}
