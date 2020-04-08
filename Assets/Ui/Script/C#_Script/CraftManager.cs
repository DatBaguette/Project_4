using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    [HideInInspector] public int[] m_RobotCraftCost = { 0, 0, 0 };
    [HideInInspector] public int[] m_RobotSize = { 0, 0, 0 };

    [SerializeField] List<Text> m_robotCost;

    [HideInInspector] public int m_choosenSize = 1;

    private void Start()
    {
        m_robotCost[0].text = "5";
        m_robotCost[1].text = "10";
        m_robotCost[2].text = "15";
    }

    public void RobotSize(int choosenSize)
    {
        m_choosenSize = choosenSize;

        switch ( choosenSize)
        {
            case 1:

                m_robotCost[0].text = "5";
                m_robotCost[1].text = "10";
                m_robotCost[2].text = "15";

                break;

            case 2:

                m_robotCost[0].text = "10";
                m_robotCost[1].text = "20";
                m_robotCost[2].text = "30";

                break;

            case 3:

                m_robotCost[0].text = "20";
                m_robotCost[1].text = "40";
                m_robotCost[2].text = "60";

                break;
        }
    }

    public enum robotType
    {
        flying,
        platform,
        destruction
    }
}
