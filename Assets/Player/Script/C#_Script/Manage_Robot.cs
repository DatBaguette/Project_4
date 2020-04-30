﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// It contain function to create robot
/// 
/// </summary>

public enum Robot_Type
{
    Flying,
    Platforme,
    Destruction
}

public class Manage_Robot : Singleton<Manage_Robot>
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

    [SerializeField]
    private GameObject m_FlyingBot_1;

    [SerializeField]
    private GameObject m_FlyingBot_2;

    [SerializeField]
    private GameObject m_FlyingBot_3;

    [SerializeField]
    private GameObject m_PlatformeBot_1;

    [SerializeField]
    private GameObject m_PlatformeBot_2;

    [SerializeField]
    private GameObject m_PlatformeBot_3;

    [SerializeField]
    private GameObject m_DestructionBot_1;

    [SerializeField]
    private GameObject m_DestructionBot_2;

    [SerializeField]
    private GameObject m_DestructionBot_3;


    [Tooltip("Array of robot price")]
    [SerializeField]
    public int[] price;
    
    public void CreateFlyingRobot()
    {
        // Use to define robot type in "RobotInisialisation" script
        //int actualRobotType = 1;

        // Condition to create robot if player have enough money
        bool createRobot = false;

        // Check if there is enough money depending on the chosen size
        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(price[0]); break;

            case 2: createRobot = CheckIfEnoughMoney(price[1]); break;

            case 3: createRobot = CheckIfEnoughMoney(price[2]); break;
        }

        // Delete ressources depending on the good amount
        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources.Value -= price[0]; break;

                case 2: GameManager.Instance.m_actualRessources.Value -= price[1]; break;

                case 3: GameManager.Instance.m_actualRessources.Value -= price[2]; break;
            }

            InstantiateRobot(m_FlyingBot_1);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }

    public void CreatePlatformRobot()
    {
        //int actualRobotType = 2;

        bool createRobot = false;

        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(price[3]); break;

            case 2: createRobot = CheckIfEnoughMoney(price[4]); break;

            case 3: createRobot = CheckIfEnoughMoney(price[5]); break;
        }

        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources.Value -= price[3]; break;

                case 2: GameManager.Instance.m_actualRessources.Value -= price[4]; break;

                case 3: GameManager.Instance.m_actualRessources.Value -= price[5]; break;
            }

            InstantiateRobot(m_PlatformeBot_1);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }

    public void CreateDestructionRobot()
    {
        //int actualRobotType = 3;

        bool createRobot = false;

        switch (m_craftManager.m_choosenSize)
        {
            case 1: createRobot = CheckIfEnoughMoney(price[6]); break;

            case 2: createRobot = CheckIfEnoughMoney(price[7]); break;

            case 3: createRobot = CheckIfEnoughMoney(price[8]); break;
        }

        if (createRobot)
        {

            switch (m_craftManager.m_choosenSize)
            {
                case 1: GameManager.Instance.m_actualRessources.Value -= price[6]; break;

                case 2: GameManager.Instance.m_actualRessources.Value -= price[7]; break;

                case 3: GameManager.Instance.m_actualRessources.Value -= price[8]; break;
            }

            InstantiateRobot(m_DestructionBot_1);
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }

    //Instantiate Robot and give him properties to work

    public void InstantiateRobot(GameObject p_prebabbot)
    {

        GameManager.Instance.m_robotNumber += 1;

        // Robot Object
        var robot = Instantiate(p_prebabbot, m_player.gameObject.transform);
        robot.transform.SetParent(m_robotContainer.transform, false);
        robot.gameObject.name = "robot " + GameManager.Instance.m_robotNumber;

        robot.transform.position = m_player.gameObject.transform.position + new Vector3(2, 0, 0);

        // Add the robot to a global list
        if (GameManager.Instance.m_robots.Count >= GameManager.Instance.m_robotNumber)
        {
            GameManager.Instance.m_robots[GameManager.Instance.m_robotNumber-1] = robot;
        }
        else
            GameManager.Instance.m_robots.Add(robot);

        // Robot Values
        RobotMovement robotScriptToMove = robot.gameObject.GetComponent<RobotMovement>();
        robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

        // Robot UI
        var robotUi = Instantiate(m_robotUIPrefab);
        robotUi.transform.SetParent(m_robotUIContainer.transform, false);

        // Add the robot UI to a global list
        if (GameManager.Instance.m_robotsUI.Count >= GameManager.Instance.m_robotNumber)
            GameManager.Instance.m_robotsUI[GameManager.Instance.m_robotNumber-1] = robotUi;
        else
            GameManager.Instance.m_robotsUI.Add(robotUi);

        ChooseRobot chooseRobotScript = robotUi.gameObject.GetComponent<ChooseRobot>();
        chooseRobotScript.m_uiRobotNumber = GameManager.Instance.m_robotNumber;
        chooseRobotScript.m_associateRobot = robot;
        chooseRobotScript.m_player = m_player;

        Button robotUiButton = robotUi.gameObject.GetComponent<Button>();
        robotUiButton.onClick.AddListener(chooseRobotScript.SelectRobot);

        var UIName = robotUi.gameObject.transform.Find("Text");

        Text UINameText = UIName.gameObject.GetComponent<Text>();
        UINameText.text = robot.gameObject.name;
    }
    //public void InstantiateRobot(RobotInisialisation RobotCreat)
    //{
    //    GameManager.Instance.m_robotNumber += 1;

    //    // Robot Object
    //    var robot = Instantiate(m_robotPrefab, m_player.gameObject.transform);
    //    robot.transform.SetParent(m_robotContainer.transform, false);
    //    robot.gameObject.name = "robot " + GameManager.Instance.m_robotNumber;
    //    robot.gameObject.transform.localScale *= m_craftManager.m_choosenSize;
    //    robot.gameObject.transform.localScale /= 1.5f;

    //    // Robot Values
    //    ClickToMoveEntity robotScriptToMove = robot.gameObject.GetComponent<ClickToMoveEntity>();
    //    robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;

    //    // Assign robot type
    //    RobotInisialisation robotTypeScript = robot.gameObject.GetComponent<RobotInisialisation>();
    //    robotTypeScript.m_robotTypeInCreation = actualRobotType;
    //    robotTypeScript.m_robotSize = m_craftManager.m_choosenSize;
    //    robotTypeScript.SelectRobotType();
    //    robotTypeScript.AssignRobotType();

    //    // Robot UI
    //    var robotUi = Instantiate(m_robotUIPrefab);
    //    robotUi.transform.SetParent(m_robotUIContainer.transform, false);

    //    ChooseRobot chooseRobotScript = robotUi.gameObject.GetComponent<ChooseRobot>();
    //    chooseRobotScript.m_uiRobotNumber = GameManager.Instance.m_robotNumber;
    //    chooseRobotScript.m_associateRobot = robot;
    //    chooseRobotScript.m_player = m_player;

    //    Button robotUiButton = robotUi.gameObject.GetComponent<Button>();
    //    robotUiButton.onClick.AddListener(chooseRobotScript.SelectRobot);

    //    var UIName = robotUi.gameObject.transform.Find("Text");

    //    Text UINameText = UIName.gameObject.GetComponent<Text>();
    //    UINameText.text = robot.gameObject.name;

    //    // Change position ( dont worl ) :(
    //    robot.gameObject.transform.position += new Vector3(127, 7, 87);
    //}

    // Check if the player have enough money in the GameManager
    private bool CheckIfEnoughMoney(int price)
    {
        if ( GameManager.Instance.m_actualRessources.Value >= price)
        {
            return true;
        }

        return false;
    }
}
