// ===============================
// AUTHOR     :         Balbona 
// CREATE DATE     :    ????
// PURPOSE     :        Make the helper lerp into the position of the other helper
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DirHelper : MonoBehaviour
{
    /// <summary>
    /// Data for the lerp
    /// </summary>
    public SC_MoveVar MoveVar;

    /// <summary>
    /// dead zone of the stick  
    /// </summary>
    private float StickDeadZone;
    /// <summary>
    /// speed to lerp
    /// </summary>
    private float f_FollowDirLerp;
    /// <summary>
    /// The D factor, I'm not really sure of how it work
    /// </summary>
    private float f_FactorD;

    /// <summary>
    /// the stick 
    /// </summary>
    public GameObject StickHelper;
    /// <summary>
    /// the mesh of the rebot us like the point 
    /// </summary>
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
