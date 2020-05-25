using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SavedCheckPoint m_saveData;

    [SerializeField] GameObject m_cinematic;
    
    public void LaunchGame()
    {
        m_cinematic.SetActive(true);
        m_cinematic.GetComponent<Animation>().Play("Buisson1");

        StartCoroutine(NextScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenBrowserForFeedbacks()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfExEdubQvAVt_tSGWCivZEKC8owTmXVLdGz6VoBYyUstQggQ/viewform?usp=sf_link");
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(10);

        m_saveData.m_actualSceneID = 1;
        SceneManager.LoadScene(m_saveData.m_actualSceneID);
    }
}
