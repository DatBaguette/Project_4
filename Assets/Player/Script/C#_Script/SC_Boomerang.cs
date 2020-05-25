using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoomerangState
{
    Off,
    ReadyToCast,
    Channeling,
    OnTravel,
    Restart
}
public class SC_Boomerang : MonoBehaviour
{
    [SerializeField]
    public GameObject m_Boomerang;
    [SerializeField]
    private GameObject helperBoomerang;
    [SerializeField]
    private GameObject Player;

    

    public BoomerangState CurrentBoomerangstat;

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

    public bool readyTochannel = true;

    [SerializeField] int m_returnSpeed = 5;

    // Start is called before the first frame update
    public void Start()
    {
        CurrentBoomerangstat = BoomerangState.Off;
    }

    public void InitBoom()
    {
        GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.boomerang;
        CurrentBoomerangstat = BoomerangState.ReadyToCast;
        nextNodeId = 0;
        readyTochannel = true;
    }

    private void Update()
    {

        CurrentTimeBetweenNode = CurrentTimeBetweenNode + Time.deltaTime;

        if (GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
        {
            if(m_Boomerang.activeInHierarchy == false)
            {
                m_Boomerang.SetActive(true);
            }

            switch (CurrentBoomerangstat)
            {              
                case BoomerangState.ReadyToCast:

                    PrepareToCast();
                    
                    break;
                        
                case BoomerangState.Channeling:
                    readyTochannel = false;
                    ChannelingTheBoomerang();
                    
                    break;
                case BoomerangState.OnTravel:

                    MouveBoomerang();
                    
                    break;
                case BoomerangState.Restart:

                    RestardBoomrangPos();
                    

                    
                    break;
            }
        }
        if(GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.boomerang)
        {
            m_Boomerang.SetActive(false);
        }

    }

    private void PrepareToCast()
    {
        if (Input.GetMouseButtonDown(0) && CurrentBoomerangstat != BoomerangState.OnTravel && readyTochannel == true)
        {
            CurrentBoomerangstat = BoomerangState.Channeling;
        }  
    }

    private void ChannelingTheBoomerang()
    {
        if (Input.GetMouseButton(0))
        {

            if (CurrentTimeBetweenNode >= DelayBetweenNode && nextNodeId < TravelNode.Length
                    && CurrentBoomerangstat != BoomerangState.OnTravel)
            {
                TravelNode[nextNodeId].transform.position = GameManager.Instance.RetrievePosition();
                //Debug.Log(GameManager.Instance.RetrievePosition());                           
                nextNodeId++;
                CurrentTimeBetweenNode = 0;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_Lunch = true;

            StartCoroutine(AnimBoomrang(1f));

            finalNode = nextNodeId - 1;
            nextNodeId = 0;
            CurrentBoomerangstat = BoomerangState.OnTravel;
        }
    }

    private IEnumerator AnimBoomrang(float WaitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitTime);

            GameManager.Instance.m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_Lunch = false;
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
        
        if (NodeTo >= TravelNode.Length || NodeFrom >= TravelNode.Length )
        {
            NodeFrom = 1;
            NodeTo = 0;
        }

        m_Boomerang.transform.position = Vector3.Lerp(TravelNode[NodeFrom].transform.position, TravelNode[NodeTo].transform.position, Perc);

        if (Perc >= 1)
        {
            if(NodeTo < TravelNode.Length)
            {
                NodeFrom++;
                NodeTo++;
            }
            
            currentLerptime = 0f;
            if (NodeTo == finalNode)
            {
                
                Restart_boomerang();
                CurrentBoomerangstat = BoomerangState.Restart;

                GameManager.Instance.m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_Lunch = false;
            }
        }
    }

    public void RestardBoomrangPos()
    {

        //GameManager.Instance.m_player.GetComponentInChildren<BIlly_Anim_CTRL>().isBoomerang_Lunch = false;

        currentLerptime += Time.deltaTime / m_returnSpeed; ;
        if (currentLerptime >= LerpTime)
        {
            currentLerptime = LerpTime;
        }
        float Perc = currentLerptime / LerpTime;

        CurrentBoomerangCD += Time.deltaTime;
        Vector3 TravPos = Vector3.Lerp(m_Boomerang.transform.position, Player.transform.position, Perc);
        m_Boomerang.transform.position = TravPos;

        if (Perc >= 1) 
        {
            CurrentBoomerangstat = BoomerangState.ReadyToCast;
        }
    }

    public void Restart_boomerang()
    {
        //GameManager.Instance.m_boomerangLaunch = false;
        nextNodeId = 0;
        NodeFrom = 0;
        NodeTo = 1;
        readyTochannel = true;
    }
}
