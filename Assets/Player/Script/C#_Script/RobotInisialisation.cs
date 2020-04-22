using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// It will assign parameter to robot depending on their type
/// 
/// </summary>

public class RobotInisialisation : MonoBehaviour
{
    public Robot_Type m_robotType;

    public int m_robotSize;

    private Rigidbody controller;

    private float m_timer = 0;

    private ClickToMoveEntity m_clickToMoveScript; 

    [HideInInspector] public GameObject m_cone;
    [SerializeField] GameObject m_conePrefabs;

    private void Start()
    {

        controller = GetComponent<Rigidbody>();

        m_clickToMoveScript = gameObject.GetComponent<ClickToMoveEntity>();


    }

    
    // Behavior depending on the robot type
    private void Update()
    {
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case Robot_Type.Platforme:

                //Nothing for the moment

                break;

            case Robot_Type.Destruction:

                if (m_timer > 0)
                {
                    m_timer -= .1f;
                    m_cone.SetActive(true);
                }
                else
                {
                    m_cone.SetActive(false);
                }

                if (Input.touchCount == 2 && GameManager.Instance.m_actualSelectedRobotNumber == m_clickToMoveScript.m_thisEntityNumber)
                {
                    m_timer = 2;

                }

                break;
        }
    }
}
