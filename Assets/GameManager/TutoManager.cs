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
    [SerializeField] GameObject m_boomerangIconHelper;
    [SerializeField] GameObject m_arrowBoomerang;

    [SerializeField] GameObject m_begginingRobotSpawner;

    [SerializeField] GameObject m_movingCloud;

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
                    m_buildIconHelper.GetComponent<Animator>().SetBool("canFlash", true);
                    break;

                case tutoState.boomerang:
                    m_boomerangIconHelper.GetComponent<Animator>().SetBool("canFlash", true);
                    break;

                case tutoState.ressources:
                    m_arrowBoomerang.GetComponent<Animator>().SetBool("canMove", true);
                    break;
            }

            m_activateNextTutoState = false;

            m_activateNextTutoState = value;
        }
    }

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
                    m_movingCloud.GetComponent<IntroMovingCloud>().Move();
                    SoundManager.Instance.m_music[3].Play();
                    break;

                case tutoState.playerMovement:
                    m_arrowToMove.GetComponent<Animator>().SetBool("canMove", false);
                    break;

                case tutoState.craft:
                    m_buildIconHelper.GetComponent<Animator>().SetBool("canFlash", false);
                    break;

                case tutoState.boomerang:
                    m_boomerangIconHelper.GetComponent<Animator>().SetBool("canFlash", false);
                    break;

                case tutoState.ressources:
                    m_arrowBoomerang.GetComponent<Animator>().SetBool("canMove", false);
                    break;
            }

            m_actualTutoState = value;
        }
    }

    void Start()
    {
        m_actualTutoState = GameManager.Instance.m_saveData.m_actualTutoStepS;

        if ( GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro && ActualTutoState == tutoState.robotMovement)
        {
            ActivateNextTutoState = true;
        }
    }
    
    void Update()
    {
        if (GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.LevelOne && ActualTutoState == tutoState.robotMovement )
        {
            ActualTutoState = tutoState.playerMovement;
            ActivateNextTutoState = true;
        }

        if (ActualTutoState == tutoState.robotMovement && MenuManager.Instance.m_Joystick.transform.GetChild(1).GetComponent<VirtualJoystick>().m_InputDirection != Vector3.zero)
        {
            ActualTutoState = tutoState.playerMovement;
        }

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            // Begin Helper's animation
            if (ActualTutoState == tutoState.playerMovement && GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.LevelOne)
            {
                ActivateNextTutoState = true;
            }

            if (ActualTutoState == tutoState.boomerang && GameManager.Instance.m_robotCore[1])
            {

                ActivateNextTutoState = true;
            }

            // Stop helper's animation

            if (ActualTutoState == tutoState.playerMovement && GameManager.Instance.m_player.GetComponent<NavMeshAgent>().velocity.magnitude > 0)
            {
                ActualTutoState = tutoState.craft;
                ActivateNextTutoState = true;
            }

            if (ActualTutoState == tutoState.craft && MenuManager.Instance.m_menuOpen)
            {
                ActualTutoState = tutoState.boomerang;
            }

            if (ActualTutoState == tutoState.boomerang && GameManager.Instance.m_currentPlayerState == GameManager.m_PlayerState.boomerang)
            {
                ActualTutoState = tutoState.ressources;
                ActivateNextTutoState = true;
            }

            if (ActualTutoState == tutoState.ressources && GameManager.Instance.m_actualRessources.Value >= 2)
            {
                ActualTutoState = tutoState.end;
            }
        }
    }

    public enum tutoState
    {
        robotMovement,
        playerMovement,
        craft,
        boomerang,
        ressources,
        end
    }
}
