// ===============================
// AUTHOR     :         Balbona 
// CREATE DATE     :    ????
// PURPOSE     :        Put the different anim on billy
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BIlly_Anim_CTRL : MonoBehaviour
{
    /// <summary>
    /// The animator of billy
    /// </summary>
    [SerializeField]
    public Animator m_Animator_Billy;

    /// <summary>
    /// 
    /// </summary>
    private NavMeshAgent m_player_NavAgent;

    /// <summary>
    /// bool for when billy walk
    /// </summary>
    public bool isWalkingPressed = false;

    /// <summary>
    /// bool for when billy take the tab
    /// </summary>
    public bool isTabletteOpen = false;

    /// <summary>
    /// bool for when billy is dead
    /// </summary>
    public bool isDead = false;

    /// <summary>
    /// bool for when billy is in boomerang mod
    /// </summary>
    public bool isBoomerang_out = false;

    /// <summary>
    /// bool for when the boomerang is lunch
    /// </summary>
    public bool isBoomerang_Lunch = false;

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

        m_Animator_Billy.SetBool("Is_BoomerangOut", isBoomerang_out);

        m_Animator_Billy.SetBool("Is_Boomerang_Lunched", isBoomerang_Lunch);

        m_Animator_Billy.SetBool("Is_Dead", isDead);
    }
}
