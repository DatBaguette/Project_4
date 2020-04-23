using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndIntroTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RobotIntro")
        {
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;
        }
    }
}
