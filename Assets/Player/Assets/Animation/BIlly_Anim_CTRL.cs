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

    // Start is called before the first frame update
    void Start()
    {
        m_player_NavAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {

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


        Debug.Log(isDead);
        m_Animator_Billy.SetBool("Is_Dead", isDead);
    }
}
