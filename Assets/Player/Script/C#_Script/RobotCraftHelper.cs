using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR     :         Balbona 
// CREATE DATE     :    ????
// PURPOSE     :        Select a helper to creat the robots in an empty place
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================
public class RobotCraftHelper : MonoBehaviour
{
    [SerializeField]
    private List<Transform> NodeCraftTransform;
    /// <summary>
    /// the current node to creat the robots
    /// </summary>
    private int m_CurrentNode = 0;
    private void Start()
    {
        Manage_Robot.Instance.NextNodeToCreatARobot = NodeCraftTransform[m_CurrentNode];
    }

    public void NextNode()
    {
        
        m_CurrentNode++;

        if(m_CurrentNode < NodeCraftTransform.Capacity)
        {
            
            Manage_Robot.Instance.NextNodeToCreatARobot = NodeCraftTransform[m_CurrentNode];       
        }
        else
        {
            
            m_CurrentNode = 0;
            Manage_Robot.Instance.NextNodeToCreatARobot = NodeCraftTransform[m_CurrentNode];

        }

        
    }
}
