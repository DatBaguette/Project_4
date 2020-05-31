// ===============================
// AUTHOR     :         Balbona 
// CREATE DATE     :    ????
// PURPOSE     :        make the helper follow the joystic position
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StickHelper : MonoBehaviour
{
    /// <summary>
    /// The data script
    /// </summary>
    public SC_MoveVar MoveVar;
    /// <summary>
    /// the Scipt to move the robot
    /// </summary>
    public RobotMovement moveBOT;


    /// <summary>
    /// A vector to convert the poisition of the stick in direction
    /// </summary>
    Vector3 Vt3_Dir;
    /// <summary>
    /// the dead zone of the sticks
    /// </summary>
    private float StickDeadZone;
    /// <summary>
    /// the mesh of the robot us as point 0
    /// </summary>
    public GameObject Robot_Mesh;

    // Start is called before the first frame update
    void Start()
    {
        StickDeadZone = MoveVar.StickDeadZone;
    }

    // Update is called once per frame
    void Update()
    {
        SetDir();
    }

    void SetDir()
    {
        if(moveBOT != null && moveBOT.m_moveJoystickScript != null)
            Vt3_Dir = new Vector3(moveBOT.m_moveJoystickScript.m_InputDirection.x, 0, moveBOT.m_moveJoystickScript.m_InputDirection.z);

        if (Robot_Mesh != null)
        {
            this.transform.position = Robot_Mesh.transform.position + (Vt3_Dir.normalized * 3);
            //Debug.Log("BAH YES");
        }
        else if (Robot_Mesh == null)
            Debug.LogWarning("SC_StickHelper - Missing References Robot_Mesh ");

    }

}
