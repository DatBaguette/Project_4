using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  
/// it will manage the price of robot in the Crafting menu
/// 
/// </summary>

public class CraftManager : Singleton<CraftManager>
{
    [HideInInspector] public int[] m_RobotCraftCost = { 0, 0, 0 };
    [HideInInspector] public int[] m_RobotSize = { 0, 0, 0 };

    [Tooltip("Text that will show the cost of each robot depending of his size")]
    [SerializeField] List<Text> m_robotCost;

    [HideInInspector] public int m_choosenSize = 1;

    private void Start()
    {
        
        m_robotCost[0].text = Manage_Robot.Instance.price[0].ToString();
        m_robotCost[1].text = Manage_Robot.Instance.price[1].ToString();
    }

    /// <summary>
    /// Update the cost of the robot on the menu
    /// </summary>
    public void RobotSize(int choosenSize)
    {
        m_choosenSize = choosenSize;

        switch ( choosenSize)
        {
            case 1:

                m_robotCost[0].text = Manage_Robot.Instance.price[0].ToString();
                m_robotCost[1].text = Manage_Robot.Instance.price[1].ToString();

                break;

            case 2:

                m_robotCost[0].text = Manage_Robot.Instance.price[2].ToString();
                m_robotCost[1].text = Manage_Robot.Instance.price[3].ToString();

                break;

            case 3:

                m_robotCost[0].text = Manage_Robot.Instance.price[4].ToString();
                m_robotCost[1].text = Manage_Robot.Instance.price[5].ToString();

                break;
        }
    }


}
