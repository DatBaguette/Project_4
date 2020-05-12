using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StickHelper : MonoBehaviour
{

    public SC_MoveVar MoveVar;
    public RobotMovement moveBOT;

    float f_ImpulseX;
    float f_ImpulseZ;
    Vector3 Vt3_Dir;

    private float StickDeadZone;

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

        float f_ImpulseX = Input.GetAxis("Horizontal");
        float f_ImpulseZ = Input.GetAxis("Vertical");

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
