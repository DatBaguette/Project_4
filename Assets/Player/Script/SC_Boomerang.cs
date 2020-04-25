﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Boomerang : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_Boomerang;
    [SerializeField]
    private GameObject helperBoomerang;
    [SerializeField]
    private GameObject Player;

    private enum m_BoomerangState
    {
        ReadyToCast,
        Channeling,
        OnTravel,
        Restart
    }

    private m_BoomerangState CurrentBoomerangstat;

    [SerializeField]
    private float DelayBetweenNode;

    private float CurrentTimeBetweenNode;

    [SerializeField]
    private GameObject[] TravelNode;

    private int nextNodeId;
    private int finalNode;
    private int NodeFrom = 0;
    private int NodeTo = 1;

    [SerializeField]
    private float BoomerangCoolDown;
    private float CurrentBoomerangCD;

    // Movement speed in units per second.
    [SerializeField]
    private float LerpTime;

    private float currentLerptime = 0f;

    private bool readyTochannel = true;

    // Start is called before the first frame update
    void Start()
    {
        CurrentBoomerangstat = m_BoomerangState.ReadyToCast;
        nextNodeId = 0;
    }



    private void Update()
    {

        //Debug.Log(CurrentBoomerangstat);
        CurrentTimeBetweenNode = CurrentTimeBetweenNode + Time.deltaTime;

        if (GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
        {
            switch (CurrentBoomerangstat)
            {
                case m_BoomerangState.ReadyToCast:

                    PrepareToCast();
                    
                    break;
                        
                case m_BoomerangState.Channeling:
                    readyTochannel = false;
                    ChannelingTheBoomerang();
                    
                    break;
                case m_BoomerangState.OnTravel:

                    MouveBoomerang();

                    break;
                case m_BoomerangState.Restart:

                    RestardBoomrangPos();

                    break;
            }
        }       
    }

    private void PrepareToCast()
    {
        if (Input.GetMouseButtonDown(0) && CurrentBoomerangstat != m_BoomerangState.OnTravel && readyTochannel == true)
        {
            CurrentBoomerangstat = m_BoomerangState.Channeling;
        }
        
    }

    private void ChannelingTheBoomerang()
    {
        if (Input.GetMouseButton(0))
        {

            if (CurrentTimeBetweenNode >= DelayBetweenNode && nextNodeId < TravelNode.Length
                    && CurrentBoomerangstat != m_BoomerangState.OnTravel)
            {
                TravelNode[nextNodeId].transform.position = GameManager.Instance.RetrievePosition();
                //Debug.Log(GameManager.Instance.RetrievePosition());                           
                nextNodeId++;
                CurrentTimeBetweenNode = 0;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            finalNode = nextNodeId - 1;
            nextNodeId = 0;
            CurrentBoomerangstat = m_BoomerangState.OnTravel;
        }
    }

    private void MouveBoomerang()
    {

        currentLerptime += Time.deltaTime;
        if (currentLerptime >= LerpTime)
        {
            currentLerptime = LerpTime;
        }
        float Perc = currentLerptime / LerpTime;

        CurrentBoomerangCD += Time.deltaTime;

        m_Boomerang.position = Vector3.Lerp(TravelNode[NodeFrom].transform.position, TravelNode[NodeTo].transform.position, Perc);

        if (Perc >= 1)
        {
            NodeFrom++;
            NodeTo++;
            currentLerptime = 0f;
            if (NodeTo == finalNode)
            {
                
                Restart_boomerang();
                CurrentBoomerangstat = m_BoomerangState.Restart;
            }
        }

        //Debug.Log("nodeFrom : " + NodeFrom + "    nodeTo : " + NodeTo);
    }

    private void RestardBoomrangPos()
    {
        currentLerptime += Time.deltaTime;
        if (currentLerptime >= LerpTime)
        {
            currentLerptime = LerpTime;
        }
        float Perc = currentLerptime / LerpTime;

        CurrentBoomerangCD += Time.deltaTime;
        Vector3 TravPos = Vector3.Lerp(m_Boomerang.position, Player.transform.position, Perc);
        m_Boomerang.position = TravPos;

        if (Perc >= 1) 
        {
            CurrentBoomerangstat = m_BoomerangState.ReadyToCast;
        }
    }

    private void Restart_boomerang()
    {
        //GameManager.Instance.m_boomerangLaunch = false;
        nextNodeId = 0;
        NodeFrom = 0;
        NodeTo = 1;
        readyTochannel = true;
    }
}
