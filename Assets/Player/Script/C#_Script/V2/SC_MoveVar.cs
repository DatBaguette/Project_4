using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MoveVar : MonoBehaviour
{

    [Header("Sensibility")]
    [Range(0f, 1f)]
    public float StickDeadZone;

    [Header("Ortientation")]
    [Range(0f, 1f)]
    public float f_FollowDirLerp;
    [Range(1f, 10f)]
    public float f_FactorD;

    [Header("Speed")]
    public float f_MaxSpeed = 10000;
    public float f_Speed = 10;
    public float f_Brake = 10;

}
