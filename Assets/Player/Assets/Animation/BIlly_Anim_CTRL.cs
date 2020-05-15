using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIlly_Anim_CTRL : MonoBehaviour
{

    [SerializeField]
    private Animator m_Animator_Billy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            m_Animator_Billy.SetBool("Is_Walking", true);
        }
    }
}
