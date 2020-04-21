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
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                gameObject.transform.localScale /= 2;
                //controller.useGravity = false;
                gameObject.tag = "FlyingRobot";

                break;

            case Robot_Type.Platforme:

                gameObject.transform.localScale += new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);
                gameObject.tag = "PlateformRobot";

                break;

            case Robot_Type.Destruction:

                gameObject.transform.localScale /= 1.5f;
                m_cone = Instantiate(m_conePrefabs, gameObject.transform.position + gameObject.transform.forward * 1.5f, new Quaternion(0, 180, 0, 1), gameObject.transform);
                gameObject.tag = "DestructionRobot";

                break;
        }

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
