using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DirHelper : MonoBehaviour
{

    public SC_MoveVar MoveVar;

    float f_ImpulseX;
    float f_ImpulseZ;
    Vector3 Vt3_Dir;

    private float StickDeadZone;
    private float f_FollowDirLerp;
    private float f_FactorD;

    public GameObject StickHelper;
    public GameObject Robot_Mesh;

    // Start is called before the first frame update
    void Start()
    {
        StickDeadZone = MoveVar.StickDeadZone;
        f_FollowDirLerp = MoveVar.f_FollowDirLerp;
        f_FactorD = MoveVar.f_FactorD;
    }

    // Update is called once per frame
    void Update()
    {
        FollowDir();
    }

    void FollowDir()
    {

        float f_ImpulseX = Input.GetAxis("Horizontal");
        float f_ImpulseZ = Input.GetAxis("Vertical");

        Vt3_Dir = new Vector3(f_ImpulseX, 0, -f_ImpulseZ);

        if ( StickHelper != null && Robot_Mesh != null)
        {

            
            this.transform.position = Vector3.Lerp(this.transform.position, StickHelper.transform.position, MoveVar.f_FollowDirLerp);

            Vector3 vt3_Temp = new Vector3(this.transform.position.x - Robot_Mesh.transform.position.x, 0, this.transform.position.z - Robot_Mesh.transform.position.z);

            this.transform.position = Robot_Mesh.transform.position + (vt3_Temp.normalized * 2);

        }
        else if (StickHelper == null || Robot_Mesh == null)
            Debug.LogWarning("SC_DirHelper - Missing References");

    }

}
