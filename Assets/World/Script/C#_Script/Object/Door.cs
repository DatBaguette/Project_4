using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject PressurePlateOfTheGate;


    [SerializeField]
    private GameObject HimSelf;

    private PressurePlate TheScript;

    private void Start()
    {
        TheScript = PressurePlateOfTheGate.GetComponent<PressurePlate>();
    }
    private void Update()
    {
        if (TheScript.GetIsOpen == true)
        {
            HimSelf.SetActive(false);
        }
    }
}
