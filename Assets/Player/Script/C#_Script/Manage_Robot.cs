using System.Collections;
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

    /// <summary>
    /// Actual selected robot in the craft menu
    /// </summary>
    public int m_actualCraftRobot;

    [SerializeField]
    private GameObject m_FlyingBot_1;

    [SerializeField]
    private GameObject m_FlyingBot_2;

    [SerializeField]
    private GameObject m_PlatformeBot_1;

    [SerializeField]
    private GameObject m_PlatformeBot_2;

    [SerializeField]
    private GameObject m_DestructionBot_1;

    [SerializeField]
    private GameObject m_DestructionBot_2;

    [SerializeField] GameObject m_joystick;
    private VirtualJoystick m_joystickScript;

    [Tooltip("Array of robot price")]
    [SerializeField]
    public int[] price;

    public bool CanYouCraft = true;
    public Transform NextNodeToCreatARobot;
    private RobotCraftHelper CraftHelper;

    [SerializeField] List<GameObject> m_robotCraftSelection;

    [SerializeField] BIlly_Anim_CTRL m_Player_Animator;

    private void Start()
    {
        CraftHelper = m_player.GetComponentInChildren<RobotCraftHelper>();

        m_joystickScript = m_joystick.GetComponent<VirtualJoystick>();

        for (int i = 0; i < m_robotCraftSelection.Count; i++)
        {
            m_robotCraftSelection[i].SetActive(false);
        }

    }
    public void ChooseRobot(int robotType)
    {
        m_actualCraftRobot = robotType;

        for ( int i=0; i<m_robotCraftSelection.Count; i++)
        {

            if ( i == robotType )
                m_robotCraftSelection[i].SetActive(true);
            else
                m_robotCraftSelection[i].SetActive(false);
        }
    }

    public void CreateRobot()
    {
        // Condition to create robot if player have enough money
        bool createRobot = false;

        // Check if there is enough money depending on the chosen size
        switch ( m_actualCraftRobot)
        {
            case 0:

                switch (m_craftManager.m_choosenSize)
                {
                    case 1: createRobot = CheckIfEnoughMoney(price[0]); break;

                    case 2: createRobot = CheckIfEnoughMoney(price[1]); break;
                }

                if (!GameManager.Instance.m_robotCore[0])
                    createRobot = false;

                break;

            case 1:

                switch (m_craftManager.m_choosenSize)
                {
                    case 1: createRobot = CheckIfEnoughMoney(price[2]); break;

                    case 2: createRobot = CheckIfEnoughMoney(price[3]); break;
                }

                if (!GameManager.Instance.m_robotCore[1])
                    createRobot = false;

                break;

            case 2:

                switch (m_craftManager.m_choosenSize)
                {
                    case 1: createRobot = CheckIfEnoughMoney(price[4]); break;

                    case 2: createRobot = CheckIfEnoughMoney(price[5]); break;
                }

                if (!GameManager.Instance.m_robotCore[2])
                    createRobot = false;

                break;
        }

        // Delete ressources depending on the good amount
        if (createRobot)
        {

            switch (m_actualCraftRobot)
            {
                case 0:

                    switch (m_craftManager.m_choosenSize)
                    {
                        case 1: GameManager.Instance.m_actualRessources.Value -= price[0]; break;

                        case 2: GameManager.Instance.m_actualRessources.Value -= price[1]; break;
                    }

                    switch (CraftManager.Instance.m_choosenSize)
                    {
                        case 1: InstantiateRobot(m_FlyingBot_1, NextNodeToCreatARobot); break;

                        case 2: InstantiateRobot(m_FlyingBot_2, NextNodeToCreatARobot); break;
                    }

                    break;

                case 1:

                    switch (m_craftManager.m_choosenSize)
                    {
                        case 1: GameManager.Instance.m_actualRessources.Value -= price[2]; break;

                        case 2: GameManager.Instance.m_actualRessources.Value -= price[3]; break;
                    }
                    switch (CraftManager.Instance.m_choosenSize)
                    {
                        case 1: InstantiateRobot(m_PlatformeBot_1, NextNodeToCreatARobot); break;

                        case 2: InstantiateRobot(m_PlatformeBot_2, NextNodeToCreatARobot); break;
                    }
                    break;
                case 2:
                    switch (m_craftManager.m_choosenSize)
                    {
                        case 1: GameManager.Instance.m_actualRessources.Value -= price[4]; break;

                        case 2: GameManager.Instance.m_actualRessources.Value -= price[5]; break;
                    }
                    switch (CraftManager.Instance.m_choosenSize)
                    {
                        case 1: InstantiateRobot(m_DestructionBot_1, NextNodeToCreatARobot); break;

                        case 2: InstantiateRobot(m_DestructionBot_2, NextNodeToCreatARobot); break;
                    }
                    break;
            }
        }

        GameManager.Instance.m_navmesh.ResetPath();
    }
    
    //Instantiate Robot and give him properties to work

    public void InstantiateRobot(GameObject p_prebabbot , Transform p_Node)
    {
        if(GameManager.Instance.m_robotNumber < 4)
        {

            GameManager.Instance.m_robotNumber += 1;
            CraftHelper.NextNode();
            SoundManager.Instance.m_robotCreat.Play();

            // Robot Object
            var robot = Instantiate(p_prebabbot, m_player.gameObject.transform);
            robot.transform.SetParent(m_robotContainer.transform, false);
            robot.gameObject.name = "robot " + GameManager.Instance.m_robotNumber;

            //robot.transform.position = m_player.gameObject.transform.position + p_Node.position ;
            robot.transform.position = p_Node.position;

            // Add the robot to a global list
            if (GameManager.Instance.m_robots.Count >= GameManager.Instance.m_robotNumber)
            {
                GameManager.Instance.m_robots[GameManager.Instance.m_robotNumber - 1] = robot;
            }
            else
                GameManager.Instance.m_robots.Add(robot);

            // Robot Infos
            RobotInisialisation m_robotInfosScript = robot.GetComponent<RobotInisialisation>();
            m_robotInfosScript.m_size = Mathf.FloorToInt(CraftManager.Instance.m_choosenSize);

            // Robot Values
            RobotMovement robotScriptToMove = robot.gameObject.GetComponent<RobotMovement>();
            robotScriptToMove.m_thisEntityNumber = GameManager.Instance.m_robotNumber;
            robotScriptToMove.m_moveJoystickScript = m_joystickScript;

            // Robot UI
            var robotUi = Instantiate(m_robotUIPrefab);
            robotUi.transform.SetParent(m_robotUIContainer.transform, false);

            // Add the robot UI to a global list
            if (GameManager.Instance.m_robotsUI.Count >= GameManager.Instance.m_robotNumber)
                GameManager.Instance.m_robotsUI[GameManager.Instance.m_robotNumber - 1] = robotUi;
            else
                GameManager.Instance.m_robotsUI.Add(robotUi);

            ChooseRobot chooseRobotScript = robotUi.gameObject.GetComponent<ChooseRobot>();
            chooseRobotScript.m_uiRobotNumber = GameManager.Instance.m_robotNumber;
            chooseRobotScript.m_associateRobot = robot;
            chooseRobotScript.m_player = m_player;
            chooseRobotScript.m_Player_Animator = m_Player_Animator;

            Button robotUiButton = robotUi.gameObject.GetComponent<Button>();
            robotUiButton.onClick.AddListener(chooseRobotScript.SelectRobot);

            switch ( m_robotInfosScript.m_size)
            {
                case 1:
                    switch ( m_robotInfosScript.m_robotType)
                    {
                        case Robot_Type.Destruction:

                            robotUi.transform.GetChild(0).gameObject.SetActive(true);

                            break;

                        case Robot_Type.Flying:

                            robotUi.transform.GetChild(2).gameObject.SetActive(true);

                            break;

                        case Robot_Type.Platforme:

                            robotUi.transform.GetChild(4).gameObject.SetActive(true);

                            break;
                    }

                    break;

                case 2:

                    switch (m_robotInfosScript.m_robotType)
                    {
                        case Robot_Type.Destruction:

                            robotUi.transform.GetChild(1).gameObject.SetActive(true);

                            break;

                        case Robot_Type.Flying:

                            robotUi.transform.GetChild(3).gameObject.SetActive(true);

                            break;

                        case Robot_Type.Platforme:

                            robotUi.transform.GetChild(5).gameObject.SetActive(true);

                            break;
                    }

                    break;
            }
        }
    }

    

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
