using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ===============================
// AUTHOR     :          Curie
// CREATE DATE     :    ????
// PURPOSE     :        Manage the change between level and the cinematique
// SPECIAL NOTES:       null
// ===============================
// Change History:      404 error not fund
//
//==================================

public class ChangeLevel : MonoBehaviour
{
    /// <summary>
    /// the next level the script have to lunch
    /// </summary>
    [SerializeField] int m_nextLevelNumber;
  
    /// <summary>
    /// the gameobject of the cinematique to lunch
    /// </summary>
    [SerializeField] GameObject m_anim;

    private void OnTriggerEnter(Collider other)
    {
        //lunch the next scene
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
                    StartCoroutine(ChangeScene(15));

                    break;

                case 4:

                    m_anim.GetComponent<Animation>().Play("Cinematic4");
                    SoundManager.Instance.m_music[GameManager.Instance.m_saveData.m_actualSceneID - 1].Stop();
                    StartCoroutine(ChangeScene(12));

                    break;

                case 5:

                    m_anim.GetComponent<Animation>().Play("Cinematic5");
                    StartCoroutine(ChangeScene(15));

                    break;

                case 6:

                    SoundManager.Instance.m_music[GameManager.Instance.m_saveData.m_actualSceneID - 2].Stop();
                    SoundManager.Instance.m_music[GameManager.Instance.m_saveData.m_actualSceneID - 1].Play();
                    m_anim.GetComponent<Animation>().Play("Cinematic6");
                    StartCoroutine(ChangeScene(38));

                    break;

            }

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
