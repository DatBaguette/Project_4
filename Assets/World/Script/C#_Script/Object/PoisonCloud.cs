using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() ))
        {
            if (GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
                GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
            else
                GameManager.Instance.m_player.transform.position = GameManager.Instance.m_actualCheckPointObject.transform.position;
        }
    }
}
