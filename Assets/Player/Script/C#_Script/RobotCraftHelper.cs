using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCraftHelper : MonoBehaviour
{
    [SerializeField]
    private List<Transform> NodeCraftTransform;
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
