using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextLanguage : MonoBehaviour
{
    public string m_french;
    public string m_english;

    public void Start()
    {
        Text text = gameObject.GetComponent<Text>();

        switch ( GameManager.Instance.m_actualLanguage)
        {
            case GameManager.Language.French:

                text.text = m_french;

                break;

            case GameManager.Language.English:

                text.text = m_english;

                break;
        }
    }
}