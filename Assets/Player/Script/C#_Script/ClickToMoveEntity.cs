using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// It will hide the interface if the character isn't controlled
/// and allow to move him
/// 
/// </summary>

public class ClickToMoveEntity : MonoBehaviour
{
    
    private NavMeshAgent m_navMeshAgent;

    [SerializeField] GameObject m_boomerangManager;
    [SerializeField] GameObject m_Joystick;

    public GameObject m_tablette;
    public BIlly_Anim_CTRL m_Player_Animator;

    /// <summary>
    /// Allow to reset the joystick position
    /// </summary>
    private Vector3 m_baseJoystickPosition;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_baseJoystickPosition = m_Joystick.transform.position;

        m_Player_Animator = gameObject.GetComponent<BIlly_Anim_CTRL>();
    }

    private void Update()
    {
        Debug.Log(GameManager.Instance.m_currentPlayerState);


        // desactivate the interface if the player dont control the character
        if (GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.move_player 
            && GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.boomerang
            && GameManager.Instance.m_currentPlayerState != GameManager.m_PlayerState.Menu
            && !MenuManager.Instance.m_parameterMenu.activeSelf)
        {
            m_boomerangManager.SetActive(false);
            m_Joystick.transform.position = m_baseJoystickPosition;
        }
        else
        {
            m_boomerangManager.SetActive(true);
            m_Joystick.transform.position = m_baseJoystickPosition - new Vector3(1000, 0, 0);
        }

        // Move the player
       // if (Input.GetMouseButtonDown(0) && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player)
       if (Imput_Manager.Instance.GetInput() == true && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.move_player)
            {

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if ( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 5))
                m_navMeshAgent.destination = GameManager.Instance.RetrievePosition();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {

            }
        }

        if (Input.GetKeyDown("k")){
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
        }
    }
}
