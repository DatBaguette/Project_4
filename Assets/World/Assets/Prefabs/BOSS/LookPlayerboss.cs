using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerboss : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    private GameObject Target;

    [SerializeField]
    private BossBehaviour CurrentBoss;

    [SerializeField]
    private GD2Lib.Vector3Var m_target_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        m_target_position.Value = Vector3.MoveTowards(Parent.transform.position, Target.transform.position, 3f);
        //m_target_position.y;
        
        Vector3 dir = m_target_position.Value;
        //dir.y = 0f;

        gameObject.transform.position = dir;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "LerpFlow")
        {
           

            CurrentBoss.Current_Boss_State = BossState.Mouvement;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LerpFlow")
        {
            

            CurrentBoss.Current_Boss_State = BossState.Rotation_On;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Parent.transform.position, m_target_position.Value);
    }
#endif
}
