using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] int m_nextLevelNumber;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<ClickToMoveEntity>())
        {
            GameManager.Instance.KillAllRobot();

            GameManager.Instance.m_saveData.m_actualSceneID = m_nextLevelNumber;
            GameManager.Instance.ActualCheckPointNumber = 0;

            GameManager.Instance.SaveData();

            SceneManager.LoadScene(m_nextLevelNumber);
        }
        else if (other.GetComponent<RobotMovement>() && GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
        {
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;

            GameManager.Instance.m_saveData.m_actualSceneID = m_nextLevelNumber;

            GameManager.Instance.SaveData();

            SoundManager.Instance.m_music[3].Stop();

            SceneManager.LoadScene(m_nextLevelNumber);
        }
        
    }
}
