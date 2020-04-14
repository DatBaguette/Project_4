using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesBehavior : MonoBehaviour
{
    [HideInInspector] private bool m_harvested;

    [Tooltip("Time to reach the player")]
    [SerializeField] float m_speed = 2f;

    [Tooltip("Main character gameObject")]
    [SerializeField] GameObject m_player;
    private Vector3 m_targetPosition;

    [Tooltip("Amount of ressources that will give the object")]
    public int m_ressourcesAmount = 10;

    private void Start()
    {
        gameObject.transform.localScale *= ( m_ressourcesAmount * 0.2f ) / 10;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "HarvestZone")
        {
            RessourcesBehavior ressourcesScript = other.gameObject.GetComponent<RessourcesBehavior>();
            m_harvested = true;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.m_actualRessources += m_ressourcesAmount;
        }
    }

    private void Update()
    {
        if ( m_harvested)
        {
            moveRessources();
        }

    }

    private void moveRessources()
    {
        m_targetPosition = m_player.transform.position;
        var directionOfTravel = m_targetPosition - gameObject.transform.position;
        transform.Translate(
                (directionOfTravel.x * m_speed * Time.deltaTime),
                (directionOfTravel.y * m_speed * Time.deltaTime),
                (directionOfTravel.z * m_speed * Time.deltaTime),
                Space.World);
        m_speed += 0.05f;
    }
}
