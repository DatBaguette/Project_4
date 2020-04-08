using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manage_Robot : MonoBehaviour
{
    [SerializeField] GameObject m_robotPrefab;
    [SerializeField] GameObject m_robotUIPrefab;

    [SerializeField] GameObject m_robotUIContainer;
    [SerializeField] GameObject m_robotContainer;
    [SerializeField] GameObject m_player;

    public void CreateFlyingRobot()
    {
        int actualRobotType = 1;

        InstantiateRobot(actualRobotType);
    }

    public void CreatePlatformRobot()
    {
        int actualRobotType = 2;

        InstantiateRobot(actualRobotType);
    }

    public void CreateDestructionRobot()
    {
        int actualRobotType = 3;

        InstantiateRobot(actualRobotType);
       
    }

    private void InstantiateRobot(int actualRobotType)
    {
        GameManager.Instance.m_robotNumber += 1;

        var robot = Instantiate(m_robotPrefab, m_player.gameObject.transform);
        robot.transform.SetParent(m_robotContainer.transform, false);
        robot.gameObject.name = "robot " + GameManager.Instance.m_robotNumber;

        ClickToMoveEntity robotScriptToMove = robot.gameObject.GetComponent<ClickToMoveEntity>();
        robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

        RobotType robotTypeScript = robot.gameObject.GetComponent<RobotType>();
        robotTypeScript.m_robotTypeInCreation = actualRobotType;
        robotTypeScript.SelectRobotType();
        robotTypeScript.AssignRobotType();

        var robotUi = Instantiate(m_robotUIPrefab);
        robotUi.transform.SetParent(m_robotUIContainer.transform, false);

        ChooseRobot chooseRobotScript = robotUi.gameObject.GetComponent<ChooseRobot>();
        chooseRobotScript.m_uiRobotNumber = GameManager.Instance.m_robotNumber;
        chooseRobotScript.m_associateRobot = robot;
        chooseRobotScript.m_player = m_player;

        Button robotUiButton = robotUi.gameObject.GetComponent<Button>();
        robotUiButton.onClick.AddListener(chooseRobotScript.SelectRobot);

        var UIName = robotUi.gameObject.transform.Find("Text");

        Text UINameText = UIName.gameObject.GetComponent<Text>();
        UINameText.text = robot.gameObject.name;

        robot.gameObject.transform.position += new Vector3(10 * GameManager.Instance.m_robotNumber, 0, 0);
    }
}
