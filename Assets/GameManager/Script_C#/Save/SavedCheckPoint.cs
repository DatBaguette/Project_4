using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckPointData", menuName = "ScriptableObjects/CheckPointData", order = 1)]
public class SavedCheckPoint : ScriptableObject
{
    [Header("Position Settings")]

    public int m_checkPointNumberS = 0;

    public int m_actualSceneID = 0;

    [Header("Steps Settings")]

    public TutoManager.tutoState m_actualTutoStepS;

    public GameManager.StoryStep m_actualStoryStepS = GameManager.StoryStep.Intro;

    [Header("Unlocks elements Settings")]

    public int m_actualRessourcesS = 0;

    public bool[] m_robotCoreS = { false, false, false };

    public bool m_sizeUnlockedS = false;

    [Header("Parameters Settings")]

    public bool m_musicOn = true;

    public bool m_soundEffectOn = true;

    public GameManager.Language m_actualLanguage = GameManager.Language.French;
}
