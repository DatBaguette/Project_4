﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] float m_baseMoveSpeed = 5.0f;
    [SerializeField] float m_drag = 0.5f;
    [SerializeField] float m_terminalRotationSpeed = 25.0f;

    private VirtualJoystick m_moveJoystickScript;
    private GameObject[] m_moveJoystick = { null };

    private Rigidbody controller;

    private float m_movementSpeed;

    public int m_thisEntityNumber;

    void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = m_terminalRotationSpeed;
        controller.drag = m_drag;

        m_movementSpeed = m_baseMoveSpeed;

        m_moveJoystick = GameObject.FindGameObjectsWithTag("Joystick");
        m_moveJoystickScript = m_moveJoystick[0].GetComponent<VirtualJoystick>();
    }

    void Update()
    {

        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        if (m_moveJoystickScript.m_InputDirection != Vector3.zero)
        {
            dir = new Vector3(m_moveJoystickScript.m_InputDirection.x, 0, m_moveJoystickScript.m_InputDirection.z);
        }

        if (GameManager.Instance.m_actualSelectedRobotNumber != m_thisEntityNumber)
            dir = new Vector3(0, 0, 0);

        //controller.AddForce(dir * m_movementSpeed, ForceMode.Impulse);

        transform.Translate(dir * m_movementSpeed);
    }
}
