using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float motorMaxForce = 100;
    public float brakeMaxForce = 100;
    public float steerMaxForce = 40;

    public Sprite[] gear;
    public Image gearUI;
    public Sprite[] handlebreak;
    public Image handlebreakUI;

    public Rigidbody car;
    public WheelCollider wheelColliderFrontRight;
    public WheelCollider wheelColliderFrontLeft;
    public WheelCollider wheelColliderRearRight;
    public WheelCollider wheelColliderRearLeft;

    private int moveForward = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (moveForward == 1) moveForward = 0;
            else if (moveForward == 2) moveForward = 1;
            Debug.Log(moveForward);
            gearUI.sprite = gear[moveForward];
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (moveForward == 0) moveForward = 1;
            else if (moveForward == 1) moveForward = 2;
            Debug.Log(moveForward);
            gearUI.sprite = gear[moveForward];
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handlebreakUI.sprite = handlebreak[1];
            wheelColliderFrontRight.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderFrontLeft.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderRearRight.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderRearLeft.brakeTorque = brakeMaxForce * 4.0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            handlebreakUI.sprite = handlebreak[0];
            wheelColliderFrontRight.brakeTorque = 0.0f;
            wheelColliderFrontLeft.brakeTorque = 0.0f;
            wheelColliderRearRight.brakeTorque = 0.0f;
            wheelColliderRearLeft.brakeTorque = 0.0f;
        }
    }

    void FixedUpdate()
    {
        float motorForce = Math.Abs(Input.GetAxis("Vertical") * motorMaxForce);

        if (moveForward == 0)
        {
            wheelColliderFrontRight.motorTorque = -motorForce;
            wheelColliderFrontLeft.motorTorque = -motorForce;
        }
        if (moveForward == 2)
        {
            wheelColliderFrontRight.motorTorque = motorForce;
            wheelColliderFrontLeft.motorTorque = motorForce;
        }

        float steerForce = Input.GetAxis("Horizontal") * steerMaxForce;
        wheelColliderFrontRight.steerAngle = steerForce;
        wheelColliderFrontLeft.steerAngle = steerForce;

        if (Input.GetAxis("Vertical") < 0)
        {
            wheelColliderFrontRight.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderFrontLeft.brakeTorque = brakeMaxForce * 4.0f;
        }
        else
        {
            wheelColliderFrontRight.brakeTorque = 0.0f;
            wheelColliderFrontLeft.brakeTorque = 0.0f;
        }
    }
}
