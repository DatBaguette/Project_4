using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// it will allow the ghost to detect the character and attack it
/// 
/// </summary>

public class GhostBehavior : MonoBehaviour
{
    /// <summary>
    /// Check if the ghost move in the direction of the character
    /// </summary>
    private bool m_chasePlayer = false;

    /// <summary>
    /// Save the spawn position to reset him
    /// </summary>
    private Vector3 m_spawnPosition;

    [SerializeField] GameObject m_player;
    
    [SerializeField] int m_speed;

    private void Start()
    {
        m_spawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        // It will move to the character
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

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.GetComponent<ClickToMoveEntity>() )
            GameManager.Instance.playerDeath();
    }

    private void OnTriggerStay(Collider other)
    {
        // the layer 11 is the one for the harvested zone
        // This variable is used to avoid to trigger with the harveste zone
        int layerMask = 1 << 11;
        layerMask = ~layerMask;

        if ( other.gameObject.tag == "Player")
        {
            gameObject.transform.LookAt(other.transform);

            RaycastHit hit;

            // It check if the ray between the character and the ghost dont touch anything
            if (!Physics.Raycast(transform.position, transform.forward, out hit, Vector3.Distance(transform.position, other.transform.position) - 1, layerMask))
            {
                m_chasePlayer = true;
            }
        }
    }

    /// <summary>
    /// Reset the positon of the ghost
    /// </summary>
    public void ResetPosition()
    {
        gameObject.transform.position = m_spawnPosition;
        m_chasePlayer = false;
    }
}
