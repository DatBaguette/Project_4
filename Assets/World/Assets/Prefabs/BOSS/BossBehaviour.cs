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

    
    private Vector3 Dir= Vector3.zero;

    [SerializeField]
    private GameObject LerpHelper;

    [SerializeField]
    private GameObject Lookhelper;

    [SerializeField]
    private GameObject CaisseToStun;

    [SerializeField]
    private ContainerBoss ContainerScript;

    [SerializeField]
    private GameObject Self;

    private Rigidbody Self_Rigidbody;

    private void Start()
    {
        Self_Rigidbody = Self.GetComponent<Rigidbody>();
    }

    private void Update()
    {


        Self.transform.LookAt(LerpHelper.transform);


        switch (Current_Boss_State)
        {
            case BossState.Ideal:

                


                break;
            case BossState.Rotation_On:

                
                
                break;
            case BossState.Mouvement:

                //Parent.transform.Translate(Vector3.forward * Time.deltaTime / 100, Space.Self);
                //Self.transform.Translate(Vector3.dir / 100, Space.Self);
                
                //transform.Translate(Vector3.forward * Time.deltaTime / 100, Space.Self);

                ContainerScript.Mouvement(f_Boss_Speed);
                
                //Self_Rigidbody.AddForce(Vector3.forward , ForceMode.Impulse );

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
