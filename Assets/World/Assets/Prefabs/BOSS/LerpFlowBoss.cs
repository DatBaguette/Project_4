using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFlowBoss : MonoBehaviour
{
    [SerializeField]
    private BossBehaviour m_bossBehaviour;

    public GameObject LookHelper;
    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateThis();
    }


    private void RotateThis()
    {
        if (m_bossBehaviour.Current_Boss_State == BossState.Rotation_On)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, LookHelper.transform.position, m_bossBehaviour.f_Boss_FollowDirLerp);

            Vector3 vt3_Temp = new Vector3(this.transform.position.x - Parent.transform.position.x, 0f, this.transform.position.z - Parent.transform.position.z).normalized;

            this.transform.position = Parent.transform.position + (vt3_Temp * 3f);


        }
        else if (LookHelper == null || this == null)
            Debug.LogWarning("SC_DirHelper - Missing References");
    }


}
