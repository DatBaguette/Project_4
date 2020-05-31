using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ===============================
// AUTHOR     :         Balbona , Curie
// CREATE DATE     :    ????
// PURPOSE     :        It will assign parameter to robot depending on their type
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================


public class RobotInisialisation : MonoBehaviour
{
    /// <summary>
    /// Robot type to change their behavior
    /// </summary>
    public Robot_Type m_robotType;

    /// <summary>
    /// Robot size
    /// </summary>
    public int m_size;

    private Rigidbody controller;

    /// <summary>
    /// FlameThrower area of attack
    /// </summary>
    [SerializeField] ParticleSystem m_fire;

    /// <summary>
    /// Cone collider to burn things
    /// </summary>
    [SerializeField] GameObject m_flameCollisonTracker;

    [SerializeField] ParticleSystem m_smoke;

    [SerializeField] RobotMovement m_robotMovementScript;

    [SerializeField] List<GameObject> m_robotDestructeurActivationHelper;

    /// <summary>
    /// the rigidbody of the robot
    /// </summary>
    private Rigidbody m_rb;

    /// <summary>
    /// the raycast return
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// Script that allow the robot to move
    /// </summary>
    private RobotMovement m_movementScript;

    private void Start()
    {

        m_rb = gameObject.GetComponent<Rigidbody>();

        controller = GetComponent<Rigidbody>();

        m_movementScript = gameObject.GetComponent<RobotMovement>();

        //Initialise the robot depending of his type
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case Robot_Type.Platforme:

                gameObject.transform.position += new Vector3(0, 2, 0);

                break;

            case Robot_Type.Destruction:

                for ( int i=0; i<m_robotDestructeurActivationHelper.Count; i++)
                {
                    m_robotDestructeurActivationHelper[i].SetActive(false);
                }

#if UNITY_ANDROID
                m_robotDestructeurActivationHelper[0].SetActive(true);
#else
                m_robotDestructeurActivationHelper[1].SetActive(true);
#endif

                gameObject.transform.position += new Vector3(0, 3, 0);

                m_fire.Stop();
                m_flameCollisonTracker.SetActive(false);

                break;
        }

    }

    
    // Behavior depending on the robot type
    private void Update()
    {
        switch (m_robotType)
        {
            case Robot_Type.Flying:

                //Nothing for the moment

                break;

            case Robot_Type.Platforme:

                if ( m_robotMovementScript.m_dir != new Vector3(0,0,0) )
                {
                    if (!m_smoke.isPlaying)
                        m_smoke.Play();
                }
                else
                    m_smoke.Stop();


                break;

            case Robot_Type.Destruction:

                if ( GameManager.Instance.m_actualSelectedRobotNumber.Value == m_movementScript.m_thisEntityNumber)
                {
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f);

                    if (hit.collider.tag == "DestructionRobot")
                    {
                        if (m_robotMovementScript != null)
                            m_robotMovementScript.Is_this_atk = true;
                        
                        m_fire.Play();
                        m_flameCollisonTracker.SetActive(true);
                        SoundManager.Instance.m_robotFlamme.Play();
                        StartCoroutine(StopFlame());
                    }

                    if (Input.GetKeyDown(KeyCode.T) )
                    {
                        if(m_robotMovementScript != null)
                            m_robotMovementScript.Is_this_atk = true;
                        
                        m_fire.Play();
                        m_flameCollisonTracker.SetActive(true);
                        SoundManager.Instance.m_robotFlamme.Play();
                        StartCoroutine(StopFlame());

                    }
                }

                if ( Input.touchCount == 1 || Input.GetKeyUp(KeyCode.T))
                {
                    m_fire.Stop();
                    m_flameCollisonTracker.SetActive(false);
                }

                break;
        }
    }

    IEnumerator StopFlame()
    {
        yield return new WaitForSeconds(2);

        m_fire.Stop();
        m_flameCollisonTracker.SetActive(false);
        m_robotMovementScript.Is_this_atk = false;
    }
}
