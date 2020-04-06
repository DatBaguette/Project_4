using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSelectionMenu : MonoBehaviour
{
    [SerializeField] GameObject m_plus;
    [SerializeField] GameObject m_minus;

    [SerializeField] GameObject m_robotOne;
    [SerializeField] GameObject m_robotTwo;
    [SerializeField] GameObject m_robotThree;

    private int m_robotNumber = 1;

    private void Start()
    {
        ResetRobotInterface();
        AddRobotInterface();
    }

    public void ResetRobotInterface()
    {
        m_plus.SetActive(false);
        m_minus.SetActive(false);
        m_robotOne.SetActive(false);
        m_robotTwo.SetActive(false);
        m_robotThree.SetActive(false);
    }

    public void AddRobotInterface()
    {
        switch (m_robotNumber)
        {
            case 1:

                m_plus.SetActive(true);
                m_robotOne.SetActive(true);

                break;

            case 2:

                m_plus.SetActive(true);
                m_minus.SetActive(true);
                m_robotOne.SetActive(true);
                m_robotTwo.SetActive(true);

                break;

            case 3:
                
                m_minus.SetActive(true);
                m_robotOne.SetActive(true);
                m_robotTwo.SetActive(true);
                m_robotThree.SetActive(true);

                break;
        }
    }

    public void AddRobot()
    {
        ResetRobotInterface();

        m_robotNumber += 1;

        AddRobotInterface();
    }

    public void DeleteRobot()
    {
        ResetRobotInterface();

        m_robotNumber -= 1;

        AddRobotInterface();
    }
}
