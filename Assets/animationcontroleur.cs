using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationcontroleur : Singleton<animationcontroleur>
{
    public Animator BillyAnimator;

    [SerializeField]public GameObject m_Player;

    //private Rigidbody m_P_rig;

    // Start is called before the first frame update
    void Start()
    {
        //m_P_rig = m_Player.GetComponent<Rigidbody>();


        BillyAnimator = GetComponent<Animator>();

    }

    

    public void BillyRun()
    {
        BillyAnimator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        //if(m_Player.transform. != Vector3.zero)
        //{
        //    Debug.Log("RUUUN !!!");
        //}



    }
}
