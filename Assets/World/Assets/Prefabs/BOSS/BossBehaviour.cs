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
public class BossBehaviour : MonoBehaviour, IFireReact
{
    

    [Header("Orientation")]
    [Range(0f, 1f)]
    public float f_Boss_FollowDirLerp;
    [Range(1f, 10f)]
    public float f_Boss_FactorD;

    [Header("Speed")]
    public float f_Boss_MaxSpeed = 10000;
    public float f_Boss_Speed = 1;
    public float f_Boss_Brake = 10;

    public BossState Current_Boss_State = BossState.Rotation_On;

    //private bool It_By_Caisse = false;

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

    private float m_Hit_timer;

    private bool CanBeHit = false;

    [SerializeField] ParticleSystem m_lowSmoke;
    [SerializeField] ParticleSystem m_highSmoke;

    [SerializeField] ParticleSystem m_lightning;

    [SerializeField] GameObject m_robotCore;

    [SerializeField] GameObject m_secondSizeBall;

    [SerializeField] Animator m_elevatorAnimations;

    [SerializeField] ParticleSystem m_flame;
    [SerializeField] ParticleSystem m_bigSMOKE;

    [SerializeField] GameObject m_rollingThings;

    [SerializeField] Animator m_robotEye;

    [SerializeField] GameObject m_bossCarcasse;

    [SerializeField] int m_Boss_Life = 1;
    private int Boss_Life
    {
        get
        {
            return m_Boss_Life;
        }
        set
        {

            switch ( m_Boss_Life)
            {
                case 1:

                    Current_Boss_State = BossState.Dead;
                    m_robotCore.SetActive(false);
                    m_highSmoke.Stop();
                    m_secondSizeBall.SetActive(true);
                    m_elevatorAnimations.Play("MoveDown");
                    m_flame.Play();
                    m_bigSMOKE.Play();
                    Destroy(m_rollingThings);

                    GameObject deadBoss = Instantiate(m_bossCarcasse, gameObject.transform);
                    deadBoss.transform.SetParent(gameObject.transform.parent);
                    deadBoss.transform.localPosition = new Vector3(0, 0, 0);
                    deadBoss.transform.localScale /= 5;
                    deadBoss.transform.LookAt(GameManager.Instance.m_player.transform);
                    Destroy(gameObject);

                    break;

                case 2:

                    m_highSmoke.Play();
                    m_lowSmoke.Stop();

                    break;

                case 3:
                    m_lowSmoke.Play();

                    break;

            }

            m_Boss_Life = value;
        }
    }

    private void Start()
    {

        Self_Rigidbody = Self.GetComponent<Rigidbody>();

        //Current_Boss_State = BossState.Dead;

    }

    void IFireReact.OnFire()
    {
        /*if (Current_Boss_State == BossState.Stuned && CanBeHit == true)
        {
            CanBeHit = false;
            Boss_Life -= 1;

        }*/

        Boss_Life -= 1;
    }
    void IFireReact.OnKillFire()
    {

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

            case BossState.Stuned:

                m_robotEye.Play("RobotEyeFlash");

                break;

            case BossState.Mouvement:
                
                ContainerScript.Mouvement(f_Boss_Speed);
                
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<ClickToMoveEntity>())
            GameManager.Instance.playerDeath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MovableObject")
        {
            //It_By_Caisse = true;
            Current_Boss_State = BossState.Stuned;
            Destroy(collision.gameObject);
            m_lightning.Play();
            StartCoroutine(StopStun());
            CanBeHit = true;
            m_robotEye.SetBool("CanBlink", true);

        }
    }

    IEnumerator StopStun()
    {
        yield return new WaitForSeconds(m_Stun_Duration);

        if ( m_Boss_Life > 0)
            Current_Boss_State = BossState.Rotation_On;
        else
            Current_Boss_State = BossState.Dead;

        m_robotEye.SetBool("CanBlink", false);
    }
}
