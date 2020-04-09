using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_craftMenu;

    [SerializeField] Text m_ressourcesText;

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
    }
}
