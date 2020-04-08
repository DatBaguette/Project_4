using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotType : MonoBehaviour
{
    public robotType m_robotType;
    public int m_robotTypeInCreation;

    private Rigidbody controller;

    private float m_timer = 0;

    private ClickToMoveEntity m_clickToMoveScript; 

    [HideInInspector] public GameObject m_cone;
    [SerializeField] GameObject m_conePrefabs;

    public enum robotType
    {
        flying,
        platform,
        destruction
    }

    public void SelectRobotType()
    {

        switch (m_robotTypeInCreation)
        {
            case 1:

                m_robotType = robotType.flying;

                break;

            case 2:

                m_robotType = robotType.platform;

                break;

            case 3:

                m_robotType = robotType.destruction;

                break;
        }

        Debug.Log(m_robotType);
    }

    public void AssignRobotType()
    {

        controller = GetComponent<Rigidbody>();

        m_clickToMoveScript = gameObject.GetComponent<ClickToMoveEntity>();

        switch (m_robotType)
        {
            case robotType.flying:

                gameObject.transform.localScale /= 2;
                controller.useGravity = false;

                break;

            case robotType.platform:

                gameObject.transform.localScale += new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);

                break;

            case robotType.destruction:

                gameObject.transform.localScale /= 1.5f;
                m_cone = Instantiate(m_conePrefabs, gameObject.transform.position + gameObject.transform.forward * 1.5f, new Quaternion(0, 180, 0, 1), gameObject.transform);

                break;
        }
    }

    private void Update()
    {
        switch (m_robotType)
        {
            case robotType.flying:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case robotType.platform:

                //Nothing for the moment

                break;

            case robotType.destruction:

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
