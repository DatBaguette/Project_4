using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerboss : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    private GameObject Target;

    private Vector3 m_Vt3_DIR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {




        m_Vt3_DIR = Target.transform.position.normalized;

        //Debug.Log(m_Vt3_DIR);
        //Debug.Log(m_Vt3_DIR.normalized);

        gameObject.transform.position = Parent.transform.position + (m_Vt3_DIR.normalized *10);
        //if (Robot_Mesh != null)
        //{
        //    this.transform.position = Robot_Mesh.transform.position + (Vt3_Dir.normalized * 3);
        //    //Debug.Log("BAH YES");
        //}
        //else if (Robot_Mesh == null)
        //    Debug.LogWarning("SC_StickHelper - Missing References Robot_Mesh ");
    }
}
