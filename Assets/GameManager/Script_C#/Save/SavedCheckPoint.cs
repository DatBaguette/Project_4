using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckPointData", menuName = "ScriptableObjects/CheckPointData", order = 1)]
public class SavedCheckPoint : ScriptableObject
{
    public int m_checkPointNumberS = 0;

    public GameManager.StoryStep m_actualStoryStepS = GameManager.StoryStep.Intro;

    public int m_actualRessourcesS = 0;

    public bool[] m_robotCoreS = { false, false, false };

    public bool[] m_sizeUnlockedS = { false, false };

    public int m_actualSceneID = 0;
}
