using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBoss : MonoBehaviour
{

    [SerializeField]
    private GameObject m_player;

 

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

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_player.transform.position);
        
    }
#endif

}
