using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro &&
            ( other.GetComponent<ClickToMoveEntity>() || other.GetComponent<RobotMovement>() ))
        {
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
        }
    }
}
