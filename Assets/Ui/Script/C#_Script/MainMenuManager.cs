using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SavedCheckPoint m_saveData;
    
    public void LaunchGame()
    {
        SceneManager.LoadScene(m_saveData.m_actualSceneID);
    }
}
