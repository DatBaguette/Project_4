using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// It will show the menu and allow to give money for prototyping
/// 
/// </summary>

public class MenuManager : MonoBehaviour
{
    [Tooltip("craft menu Manager")]
    [SerializeField] GameObject m_craftMenu;

    [Tooltip("Text gameObject that will show the actual amount of ressources")]
    [SerializeField] Text m_ressourcesText;

    [SerializeField] List<GameObject> m_CraftMenuRobotTypeArea;

    [SerializeField] List<GameObject> m_sizeArea;

    public void OpenCraftMenu()
    {
        if ( m_craftMenu.activeSelf)
        {
            m_craftMenu.SetActive(false);
        }
        else
        {
            m_craftMenu.SetActive(true);
        }
    }

    public void AddRessources()
    {
        GameManager.Instance.m_actualRessources += 100;
    }

    public void DeleteRessources()
    {
        if (GameManager.Instance.m_actualRessources > 100)
        {
            GameManager.Instance.m_actualRessources -= 100;
        }
        else
        {
            GameManager.Instance.m_actualRessources = 0;
        }
    }

    private void Update()
    {
        m_ressourcesText.text = "Pièces détachées : " + GameManager.Instance.m_actualRessources.ToString();

        for ( int i = 0; i < m_CraftMenuRobotTypeArea.Count; i++)
        {
             if (!GameManager.Instance.m_robotCore[i])
                 m_CraftMenuRobotTypeArea[i].SetActive(false);

             else
                 m_CraftMenuRobotTypeArea[i].SetActive(true);
        }

        for (int i = 0; i < m_sizeArea.Count; i++)
        {
            if (!GameManager.Instance.m_sizeUnlocked[i])
                m_sizeArea[i].SetActive(false);

            else
                m_sizeArea[i].SetActive(true);
        }
    }
}
