using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ContainerBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Child;

    [SerializeField]
    private GD2Lib.Vector3Var SyncDir;

    [SerializeField]
    private GameObject m_player;

    private Vector3 TrueDir;

    private NavMeshAgent m_navMeshAgent;

    private Rigidbody Self;

    // Update is called once per frame
    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        Self = this.GetComponent<Rigidbody>();
       // this.transform.position = m_Child.transform.position;
    }

    public void Mouvement(float speed)
    {


        var m_targetPosition = m_player.transform.position;
        var directionOfTravel = m_targetPosition - gameObject.transform.position;
        directionOfTravel = directionOfTravel.normalized;
        transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);


        ////m_navMeshAgent.destination;



        ////Vector3 TrueDir = SyncDir.Value.normalized;
        ////m_target_position.Value = Vector3.MoveTowards(Parent.transform.position, Target.transform.position, 10f);

        //Vector3 Translate = Vector3.MoveTowards(transform.position, Target.transform.position, 1f);
        ////TrueDir.x = Translate.z;
        ////TrueDir.y = 0;
        ////TrueDir.z = Translate.x;

        ////Debug.Log(TrueDir);

        //Self.AddForce(Translate.normalized * speed/100, ForceMode.Impulse);

        ////transform.Translate(-TrueDir * speed * Time.deltaTime, Space.Self);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_player.transform.position);
        
    }
#endif

}
