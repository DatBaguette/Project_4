using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Boomerang : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_Boomerang;
    [SerializeField]
    private GameObject helperBoomerang;

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

    [SerializeField]
    private float BoomerangCoolDown;
    private float CurrentBoomerangCD;




    /// ////////////////////

    // Movement speed in units per second.
    private float LerpTime = 3f;

    private float currentLerptime = 0f;

    /// ////////////////////


    // Start is called before the first frame update
    void Start()
    {
        CurrentBoomerangstat = m_BoomerangState.ReadyToCast;
        nextNodeId = 0;

        

    }



    private void Update()
    {
        CurrentTimeBetweenNode = CurrentTimeBetweenNode + Time.deltaTime;

        if (Input.GetMouseButton(0) && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
        {
            CurrentBoomerangstat = m_BoomerangState.Channeling;

            //helperBoomerang.transform.position = GameManager.Instance.RetrievePosition();

            //m_Boomerang.MovePosition(helperBoomerang.transform.position);

            if (CurrentTimeBetweenNode >= DelayBetweenNode && nextNodeId<TravelNode.Length && CurrentBoomerangstat != m_BoomerangState.OnTravel)
            {
                TravelNode[nextNodeId].transform.position = GameManager.Instance.RetrievePosition();
                nextNodeId++;
                CurrentTimeBetweenNode = 0;
            }
        }

        

        if (Input.GetMouseButtonUp(0) && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
        {
            CurrentBoomerangstat = m_BoomerangState.OnTravel;
            finalNode = nextNodeId - 1;
            nextNodeId = 0;
        }

    }

    private void FixedUpdate()
    {

        

        if (CurrentBoomerangstat == m_BoomerangState.OnTravel)
        {
            currentLerptime += Time.deltaTime;
            if(currentLerptime >= LerpTime)
            {
                currentLerptime = LerpTime;
            }

            float Perc = currentLerptime / LerpTime;

            //for (int i = 0; i < finalNode; i++)
            //{
            m_Boomerang.position = Vector3.Lerp(TravelNode[0].transform.position, TravelNode[4].transform.position, Perc);
           // }
        }
    }
}
