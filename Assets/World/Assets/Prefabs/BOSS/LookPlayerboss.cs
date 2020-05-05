using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerboss : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    private GameObject Target;

    private Vector3 m_target_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        m_target_position = Vector3.MoveTowards(Parent.transform.position, Target.transform.position, 10f);
        m_target_position.y = 0;
        gameObject.transform.position = m_target_position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Parent.transform.position, Target.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Parent.transform.position, m_target_position);
    }

}
