using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BIlly_Anim_CTRL : MonoBehaviour
{
    public enum BillyAnimStep
    {
        Walk,
        Run,
        Idle,
        Tab,
        Death,
        Fear1,
        Fear2,
        Push,
    }

    [SerializeField]
    public Animator m_Animator_Billy;

    public BillyAnimStep CurrentBillyAnimStep;

    private NavMeshAgent m_player_NavAgent;

    // Start is called before the first frame update
    void Start()
    {
        m_player_NavAgent = GetComponent<NavMeshAgent>();
        CurrentBillyAnimStep = BillyAnimStep.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player_NavAgent.isStopped)
        {
            CurrentBillyAnimStep = BillyAnimStep.Idle;
        }

        if(m_player_NavAgent.velocity.magnitude > 0)
        {
            CurrentBillyAnimStep = BillyAnimStep.Walk;
        }

        if (m_player_NavAgent.velocity.magnitude > 4)
        {
            CurrentBillyAnimStep = BillyAnimStep.Run;
        }


        switch (CurrentBillyAnimStep)
        {
            case BillyAnimStep.Run:

                m_Animator_Billy.SetBool("Is_Running", true);

                Debug.Log(CurrentBillyAnimStep);

                break;
            case BillyAnimStep.Walk:

                m_Animator_Billy.SetBool("Is_Walking", true);

                Debug.Log(CurrentBillyAnimStep);

                break;
            case BillyAnimStep.Idle:

                if(m_Animator_Billy.GetBool("Is_Walking"))
                    m_Animator_Billy.SetBool("Is_Walking", false);

                if (m_Animator_Billy.GetBool("Is_Running"))
                    m_Animator_Billy.SetBool("Is_Running", false);

                Debug.Log(CurrentBillyAnimStep);

                break;
        }
    }
}
