using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Ressources behavior adapted on robots core :)
/// 
/// </summary>

public class RobotCoreBehavior : MonoBehaviour
{
    [HideInInspector] private bool m_harvested;

    [Tooltip("Time to reach the player")]
    [SerializeField] float m_speed = 2f;

    [Tooltip("Main character gameObject")]
    [SerializeField] GameObject m_player;
    private Vector3 m_targetPosition;

    [Tooltip("Amount of ressources that will give the object")]
    [SerializeField] CoreType m_coretype;

    [SerializeField] Animator m_robotCoreHelper;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "HarvestZone")
        {
            m_harvested = true;

            Debug.Log("ta mere la pute");

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (m_coretype)
            {
                case CoreType.flyingRobot:
                    
                    GameManager.Instance.m_robotCore[0] = true;

                    break;

                case CoreType.platformRobot:

                    GameManager.Instance.m_robotCore[1] = true;

                    break;

                case CoreType.destructionRobot:

                    GameManager.Instance.m_robotCore[2] = true;

                    break;

                case CoreType.size:

                    GameManager.Instance.m_sizeUnlocked = true;

                    break;
            }

            m_robotCoreHelper.Play("Show");

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (m_harvested)
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

    enum CoreType
    {
        flyingRobot,
        platformRobot,
        destructionRobot,
        size
    }
}
