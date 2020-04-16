using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  
/// it will manage the price of robot in the Crafting menu
/// 
/// </summary>

public class CraftManager : MonoBehaviour
{
    [HideInInspector] public int[] m_RobotCraftCost = { 0, 0, 0 };
    [HideInInspector] public int[] m_RobotSize = { 0, 0, 0 };

    [Tooltip("Text that will show the cost of each robot depending of his size")]
    [SerializeField] List<Text> m_robotCost;

    [HideInInspector] public int m_choosenSize = 1;

    private void Start()
    {
        m_robotCost[0].text = "50";
        m_robotCost[1].text = "100";
        m_robotCost[2].text = "150";
    }

    public void RobotSize(int choosenSize)
    {
        m_choosenSize = choosenSize;

        switch ( choosenSize)
        {
            case 1:

                m_robotCost[0].text = "50";
                m_robotCost[1].text = "100";
                m_robotCost[2].text = "150";

                break;

            case 2:

                m_robotCost[0].text = "100";
                m_robotCost[1].text = "200";
                m_robotCost[2].text = "300";

                break;

            case 3:

                m_robotCost[0].text = "200";
                m_robotCost[1].text = "400";
                m_robotCost[2].text = "600";

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
