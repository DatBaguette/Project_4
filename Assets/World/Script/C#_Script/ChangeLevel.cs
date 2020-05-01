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
            SceneManager.LoadScene(m_nextLevelNumber);

            GameManager.Instance.m_saveData.m_actualSceneID = m_nextLevelNumber;
        }
        
    }
}
