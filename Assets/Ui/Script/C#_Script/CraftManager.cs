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

    [SerializeField]
    private List<int> getPrice;

    [SerializeField]
    private Manage_Robot CurrentManager;

    private void Start()
    {
        //getPrice[] = gameObject.GetComponent<Manage_Robot>().price[];

        for(int i = 0; i < getPrice.Count ; i++)
        {
            getPrice[i] = CurrentManager.price[i];
        }


        m_robotCost[0].text = getPrice[0].ToString();
        m_robotCost[1].text = getPrice[1].ToString();
        m_robotCost[2].text = getPrice[2].ToString();
    }

    public void RobotSize(int choosenSize)
    {
        m_choosenSize = choosenSize;

        switch ( choosenSize)
        {
            case 1:

                m_robotCost[0].text = getPrice[0].ToString();
                m_robotCost[1].text = getPrice[1].ToString();
                m_robotCost[2].text = getPrice[2].ToString();

                break;

            case 2:

                m_robotCost[0].text = getPrice[3].ToString();
                m_robotCost[1].text = getPrice[4].ToString();
                m_robotCost[2].text = getPrice[5].ToString();

                break;

            case 3:

                m_robotCost[0].text = getPrice[6].ToString();
                m_robotCost[1].text = getPrice[7].ToString();
                m_robotCost[2].text = getPrice[8].ToString();

                break;
        }
    }


}
