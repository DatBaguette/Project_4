using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

/// <summary>
/// 
/// This script will manage all the tutoriel
/// 
/// The update function will detect if an animation must be start or stop
/// And the properties variables will activate the animation once
/// 
/// </summary>

public class TutoManager : MonoBehaviour
{
    [SerializeField] GameObject m_joystickHelper;
    [SerializeField] GameObject m_arrowToMove;
    [SerializeField] GameObject m_buildIconHelper;

    [SerializeField] GameObject m_begginingRobotSpawner;

    public tutoState m_actualTutoState;
    public tutoState ActualTutoState
    {
        get
        {
            return m_actualTutoState;
        }
        set
        {
            switch (ActualTutoState)
            {
                case tutoState.robotMovement:
                    m_joystickHelper.GetComponent<Animator>().SetBool("canFlash", false);
                    break;

                case tutoState.playerMovement:
                    m_arrowToMove.GetComponent<Animator>().SetBool("canMove", false);
                    break;

                case tutoState.craft:
                    m_buildIconHelper.GetComponent<Animator>().SetBool("CanFlash", false);
                    break;

                case tutoState.core:
                    // Desactivate helper on core
                    break;

                case tutoState.boomerang:
                    // Desactivate helper around ressources
                    break;
            }

            m_actualTutoState = value;
        }
    }

    public bool m_activateNextTutoState = false;
    public bool ActivateNextTutoState
    {
        get
        {
            return m_activateNextTutoState;
        }
        set
        {

            switch ( ActualTutoState )
            {
                case tutoState.robotMovement:
                    m_joystickHelper.GetComponent<Animator>().SetBool("canFlash", true);
                    break;

                case tutoState.playerMovement:
                    m_arrowToMove.GetComponent<Animator>().SetBool("canMove", true);
                    break;

                case tutoState.craft:
                    m_buildIconHelper.GetComponent<Animator>().SetBool("CanFlash", true);
                    break;

                case tutoState.core:
                    // Activate helper on core
                    break;

                case tutoState.boomerang:
                    // Activate helper around ressources
                    break;
            }

            m_activateNextTutoState = false;

            m_activateNextTutoState = value;
        }
    }

    void Start()
    {
        if ( GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
        {
            ActivateNextTutoState = true;
        }
    }
    
    void Update()
    {
        // Begin Helper's animation
        if (ActualTutoState == tutoState.playerMovement && GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.LevelOne)
        {
            ActivateNextTutoState = true;
        }

        // Stop helper's animation
        if (ActualTutoState == tutoState.robotMovement && m_begginingRobotSpawner.transform.GetChild(0).GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            ActualTutoState = tutoState.playerMovement;
        }

        if (ActualTutoState == tutoState.playerMovement && GameManager.Instance.m_player.GetComponent<NavMeshAgent>().velocity.magnitude > 0)
        {
            ActualTutoState = tutoState.craft;
            ActivateNextTutoState = true;
        }

        if (ActualTutoState == tutoState.craft && m_begginingRobotSpawner.transform.GetChild(0).GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            ActualTutoState = tutoState.core;
        }
    }

    public enum tutoState
    {
        robotMovement,
        playerMovement,
        craft,
        core,
        boomerang,
        ressources,
    }
}
