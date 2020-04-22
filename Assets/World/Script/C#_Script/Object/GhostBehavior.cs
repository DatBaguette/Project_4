using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    private bool m_canEnter = true;

    private bool m_chasePlayer = false;

    [SerializeField] GameObject m_player;

    [SerializeField] int m_speed;

    private void Update()
    {
        if ( m_chasePlayer)
        {
            var m_targetPosition = m_player.transform.position;
            var directionOfTravel = m_targetPosition - gameObject.transform.position;
            directionOfTravel = directionOfTravel.normalized;
            transform.Translate(
                    (directionOfTravel.x * m_speed * Time.deltaTime),
                    (directionOfTravel.y * m_speed * Time.deltaTime),
                    (directionOfTravel.z * m_speed * Time.deltaTime),
                    Space.World);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        int layerMask = 1 << 11;
        layerMask = ~layerMask;

        if ( other.gameObject.tag == "Player")
        {
            gameObject.transform.LookAt(other.transform);

            RaycastHit hit;

            if (!Physics.Raycast(transform.position, transform.forward, out hit, Vector3.Distance(transform.position, other.transform.position) - 1, layerMask))
            {
                m_chasePlayer = true;
            }

            Debug.DrawRay(transform.position, transform.forward * ( Vector3.Distance(transform.position, other.transform.position) - 1 ), Color.white);
        }
    }
}
