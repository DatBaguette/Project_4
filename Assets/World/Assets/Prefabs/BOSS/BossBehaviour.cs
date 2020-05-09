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
    private GameObject CaisseToStun;

    [SerializeField]
    private ContainerBoss ContainerScript;

    [SerializeField]
    private GameObject Self;

    private Rigidbody Self_Rigidbody;

    [SerializeField]
    private float m_Stun_Duration;

    private float m_Current_Stun_Duration;


    private void Start()
    {

        Self_Rigidbody = Self.GetComponent<Rigidbody>();
        m_Current_Stun_Duration = m_Stun_Duration;

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
                
                ContainerScript.Mouvement(f_Boss_Speed);
                
                break;
            case BossState.Stuned:

                if(m_Current_Stun_Duration >= 0)
                {
                    m_Current_Stun_Duration -= Time.deltaTime;
                    Debug.Log(m_Current_Stun_Duration);
                }
                else
                {
                    m_Current_Stun_Duration = m_Stun_Duration;
                    Current_Boss_State = BossState.Rotation_On;
                    Debug.Log("Stun end");
                }

                break;
            case BossState.Dead:



                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == CaisseToStun.gameObject.activeSelf)
        {
            It_By_Caisse = true;
            Current_Boss_State = BossState.Stuned;
            Destroy(collision.gameObject);
            
        }
    }
}
