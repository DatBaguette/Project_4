using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// It will move the ressources to the player if it reaches the trigger zone
/// and add the good amount of ressources
/// 
/// </summary>

public class RessourcesBehavior : MonoBehaviour
{
    /// <summary>
    /// the ressource will move if it is harvested
    /// </summary>
    [HideInInspector] public bool m_harvested;

    [Tooltip("Time to reach the player")]
    [SerializeField] float m_speed = 2f;

    [Tooltip("Main character gameObject")]
    private Vector3 m_targetPosition;

    [Tooltip("Amount of ressources that will give the object")]
    public int m_ressourcesAmount = 10;

    private void Start()
    {
        // Adapt the ressources size of his amount
        if ( m_ressourcesAmount > 0)
            gameObject.transform.localScale *= ( m_ressourcesAmount * 0.5f ) / 10 + 1;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "HarvestZone")
        {
            m_harvested = true;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.m_actualRessources.Value += m_ressourcesAmount;
            GameManager.Instance.m_nbRessourcesSinceLastCheckpoint += m_ressourcesAmount;
            MenuManager.Instance.ressourcesRetrieve += 5;
            MenuManager.Instance.m_ressourcesText.color = Color.red;
        }
    }

    private void Update()
    {
        if ( m_harvested)
        {
            moveRessources();
        }

    }

    // Move the ressources to the player
    private void moveRessources()
    {
        m_targetPosition = GameManager.Instance.m_player.transform.position;
        var directionOfTravel = m_targetPosition - gameObject.transform.position;
        transform.Translate(
                (directionOfTravel.x * m_speed * Time.deltaTime),
                (directionOfTravel.y * m_speed * Time.deltaTime),
                (directionOfTravel.z * m_speed * Time.deltaTime),
                Space.World);
        m_speed += 0.05f;
    }
}
