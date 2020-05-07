using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
Ideal,
Rotation_On,
Stuned,
Dead,
Mouvement
}
public class BossBehaviour : MonoBehaviour
{
    

    [Header("Ortientation")]
    [Range(0f, 1f)]
    public float f_Boss_FollowDirLerp;
    [Range(1f, 10f)]
    public float f_Boss_FactorD;

    [Header("Speed")]
    public float f_Boss_MaxSpeed = 10000;
    public float f_Boss_Speed = 1;
    public float f_Boss_Brake = 10;

    public BossState Current_Boss_State = BossState.Rotation_On;

    private bool It_By_Caisse = false;

    [SerializeField]
    private GameObject LerpHelper;

    [SerializeField]
    private GameObject Lookhelper;

    [SerializeField]
    private GameObject CaisseToStun;

    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    private GameObject Self;

    private void Update()
    {





        switch (Current_Boss_State)
        {
            case BossState.Ideal:

                


                break;
            case BossState.Rotation_On:

                Self.transform.LookAt(LerpHelper.transform);
                
                break;
            case BossState.Mouvement:

                Vector3 dir = Vector3.MoveTowards(Self.transform.position, Lookhelper.transform.position, 5f).normalized;
                Debug.Log(dir);

                Parent.transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);

                break;
            case BossState.Stuned:



                break;
            case BossState.Dead:



                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == CaisseToStun)
        {
            It_By_Caisse = true;
        }
    }


}
