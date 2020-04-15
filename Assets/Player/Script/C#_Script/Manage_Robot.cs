using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manage_Robot : MonoBehaviour
{
    [Tooltip("Prefab of the base robot")]
    [SerializeField] GameObject m_robotPrefab;

    [Tooltip("Prefab that will allow the player to select his robot")]
    [SerializeField] GameObject m_robotUIPrefab;

    [Tooltip("GameObject that will contain all the Robot UI Instance")]
    [SerializeField] GameObject m_robotUIContainer;

    [Tooltip("GameObject that will contain all the robot gameObject")]
    [SerializeField] GameObject m_robotContainer;

    [Tooltip("The main character gameObject")]
    [SerializeField] GameObject m_player;

    [Tooltip("Craft manager gameObject")]
    [SerializeField] CraftManager m_craftManager;
    
    public void CreateFlyingRobot()
    {
        int actualRobotType = 1;

        bool createRobot = false;

        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(50); break;

            case 2: createRobot = CheckIfEnoughMoney(100); break;

            case 3: createRobot = CheckIfEnoughMoney(150); break;
        }

        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources -= 50; break;

                case 2: GameManager.Instance.m_actualRessources -= 100; break;

                case 3: GameManager.Instance.m_actualRessources -= 150; break;
            }

            InstantiateRobot(actualRobotType);
        }
    }

    public void CreatePlatformRobot()
    {
        int actualRobotType = 2;

        bool createRobot = false;

        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(100); break;

            case 2: createRobot = CheckIfEnoughMoney(200); break;

            case 3: createRobot = CheckIfEnoughMoney(400); break;
        }

        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources -= 100; break;

                case 2: GameManager.Instance.m_actualRessources -= 200; break;

                case 3: GameManager.Instance.m_actualRessources -= 400; break;
            }

            InstantiateRobot(actualRobotType);
        }
    }

    public void CreateDestructionRobot()
    {
        int actualRobotType = 3;

        bool createRobot = false;

        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(150); break;

            case 2: createRobot = CheckIfEnoughMoney(300); break;

            case 3: createRobot = CheckIfEnoughMoney(600); break;
        }

        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources -= 150; break;

                case 2: GameManager.Instance.m_actualRessources -= 300; break;

                case 3: GameManager.Instance.m_actualRessources -= 600; break;
            }

            InstantiateRobot(actualRobotType);
        }

    }

    private void InstantiateRobot(int actualRobotType)
    {
        GameManager.Instance.m_robotNumber += 1;

        var robot = Instantiate(m_robotPrefab, m_player.gameObject.transform);
        robot.transform.SetParent(m_robotContainer.transform, false);
        robot.gameObject.name = "robot " + GameManager.Instance.m_robotNumber;
        robot.gameObject.transform.localScale *= m_craftManager.m_choosenSize;
        robot.gameObject.transform.localScale /= 1.5f;

        ClickToMoveEntity robotScriptToMove = robot.gameObject.GetComponent<ClickToMoveEntity>();
        robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

        RobotType robotTypeScript = robot.gameObject.GetComponent<RobotType>();
        robotTypeScript.m_robotTypeInCreation = actualRobotType;
        robotTypeScript.m_robotSize = m_craftManager.m_choosenSize;
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

    private bool CheckIfEnoughMoney(int price)
    {
        if ( GameManager.Instance.m_actualRessources >= price)
        {
            return true;
        }

        return false;
    }
}
