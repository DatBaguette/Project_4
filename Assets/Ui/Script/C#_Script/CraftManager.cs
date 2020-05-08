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

    [Tooltip("Text that will show the cost of each robot depending of his size")]
    [SerializeField] Text m_robotCost;

    [HideInInspector] public float m_choosenSize = 1;

    [SerializeField] Slider m_sizeSlider;

    private void Start()
    {

        RobotCostChange();
    }

    /// <summary>
    /// Update the cost of the robot on the menu
    /// </summary>
    public void RobotCostChange()
    {
        m_choosenSize = m_sizeSlider.value;

        switch ( Manage_Robot.Instance.m_actualCraftRobot )
        {
            case 0:

                if ( m_sizeSlider.value == 1 )
                    m_robotCost.text = Manage_Robot.Instance.price[0].ToString();
                else
                    m_robotCost.text = Manage_Robot.Instance.price[1].ToString();

                break;

            case 1:

                if (m_sizeSlider.value == 1)
                    m_robotCost.text = Manage_Robot.Instance.price[2].ToString();
                else
                    m_robotCost.text = Manage_Robot.Instance.price[3].ToString();

                break;

            case 2:

                if (m_sizeSlider.value == 1)
                    m_robotCost.text = Manage_Robot.Instance.price[4].ToString();
                else
                    m_robotCost.text = Manage_Robot.Instance.price[5].ToString();

                break;
        }
    }


}
