using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( ( other.GetComponent<RobotMovement>() ))
        {
            if (GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
            {
                GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
                GameManager.Instance.m_actualRessources = 0;
            }
            else
            {
                GameManager.Instance.KillOneRobot(other.gameObject.GetComponent<RobotMovement>().m_thisEntityNumber - 1);

                GameManager.Instance.m_camera.Follow = GameManager.Instance.m_player.transform;
                GameManager.Instance.m_camera.LookAt = GameManager.Instance.m_player.transform;

                GameManager.Instance.m_currentPlayerState = GameManager.m_PlayerState.move_player;
            }
        }
    }
}
