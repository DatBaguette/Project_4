using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BIlly_Anim_CTRL : MonoBehaviour
{
  
    [SerializeField]
    public Animator m_Animator_Billy;

    private NavMeshAgent m_player_NavAgent;

    public bool isWalkingPressed = false;

    public bool isTabletteOpen = false;

    public bool isDead = false;

    public bool isBoomerang_out = false;

    public bool isBoomerang_Lunch = false;

    // Start is called before the first frame update
    void Start()
    {
        m_player_NavAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
        //{
        //    isBoomerang_out = true;

        //    Debug.Log("1");
        //}
        //else
        //{
        //    isBoomerang_out = false;

        //    Debug.Log("2");
        //}

        if (m_player_NavAgent.velocity.magnitude > 0.3f)
        {
            isWalkingPressed = true;

            //Debug.Log("Mouve");
        }
        else
        {
            isWalkingPressed = false;

           // Debug.Log("Stop");
        }

        m_Animator_Billy.SetBool("Is_Running", isWalkingPressed);

        m_Animator_Billy.SetBool("Is_Tab_out", isTabletteOpen);

        m_Animator_Billy.SetBool("Is_BoomerangOut", isBoomerang_out);

        m_Animator_Billy.SetBool("Is_Boomerang_Lunched", isBoomerang_Lunch);

        m_Animator_Billy.SetBool("Is_Dead", isDead);
    }
}
