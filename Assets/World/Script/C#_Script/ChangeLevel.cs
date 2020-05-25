using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] int m_nextLevelNumber;

    [SerializeField] GameObject m_anim;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<ClickToMoveEntity>())
        {
            GameManager.Instance.KillAllRobot();

            GameManager.Instance.m_saveData.m_actualSceneID = m_nextLevelNumber;
            GameManager.Instance.ActualCheckPointNumber = 0;

            GameManager.Instance.SaveData();

            m_anim.SetActive(true);

            switch ( GameManager.Instance.m_saveData.m_actualSceneID)
            {
                case 3:
                    
                    m_anim.GetComponent<Animation>().Play("Cinematic3");

                    break;
            }

            StartCoroutine(ChangeScene(15));

        }
        else if (other.GetComponent<RobotMovement>() && GameManager.Instance.m_actualStoryStep == GameManager.StoryStep.Intro)
        {
            GameManager.Instance.m_actualStoryStep = GameManager.StoryStep.LevelOne;

            GameManager.Instance.m_saveData.m_actualSceneID = m_nextLevelNumber;

            GameManager.Instance.SaveData();

            m_anim.SetActive(true);
            m_anim.GetComponent<Animation>().Play("Cinematic2");

            StartCoroutine(ChangeScene(8));
        }
        
    }

    IEnumerator ChangeScene(int time)
    {
        yield return new WaitForSeconds(time);

        SoundManager.Instance.m_music[3].Stop();

        SceneManager.LoadScene(m_nextLevelNumber);
    }
}
