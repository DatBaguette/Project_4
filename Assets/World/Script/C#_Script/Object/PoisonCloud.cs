using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// Behavior of the poison cloud that kill robots.
/// 
/// </summary>

public class PoisonCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If it touch a robot
        if ( ( other.GetComponent<RobotMovement>() ))
        {
            //End the intro if it's the actual story's step
            if (GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
            {
                GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
                GameManager.Instance.m_actualRessources.Value = 0;
            }
            else //Kill the robot and return to the main character
            {
                GameManager.Instance.KillOneRobot(other.gameObject.GetComponent<RobotMovement>().m_thisEntityNumber - 1);

                GameManager.Instance.m_camera.Follow = GameManager.Instance.m_player.transform;
                GameManager.Instance.m_camera.LookAt = GameManager.Instance.m_player.transform;

                GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_player;

                GameManager.Instance.m_robotNumber = 0;
            }
        }
    }
}
