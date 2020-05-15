using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SavedCheckPoint m_saveData;
    
    public void LaunchGame()
    {
        m_saveData.m_actualSceneID = 1;
        SceneManager.LoadScene(m_saveData.m_actualSceneID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenBrowserForFeedbacks()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeACeRAw54Zd0whN3faM3H6IcdtmiYor_04sxPtPw867ZVm3A/viewform?usp=sf_link");
    }
}
