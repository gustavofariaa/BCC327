using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float motorMaxForce = 100;

    public float brakeMaxForce = 100;

    public float steerMaxForce = 40;

    public Sprite[] gear;

    public Sprite[] handlebreak;

    public Sprite[] result;

    public Sprite[] objective;

    public Sprite[] belt;

    public Image gearUI;

    public Image handlebreakUI;

    public Image resultUI;

    public Image objectiveUI;

    public Image beltUI;
    public Image hintUI;

    public Rigidbody car;

    public GameObject firstStepFront;

    public GameObject firstStepBack;

    public GameObject secondStepFront;

    public GameObject secondStepBack;

    public WheelCollider wheelColliderFrontRight;

    public WheelCollider wheelColliderFrontLeft;

    public WheelCollider wheelColliderRearRight;

    public WheelCollider wheelColliderRearLeft;

    private int moveForward = 1;

    private bool showHudElements = true;

    private bool isHandlebrakeUp = true;

    private bool isBeltFastened = false;

    private bool isFirstStep = true;

    private bool firstStepBackWasCollider = false;

    private bool secondStepBackWasCollider = false;

    void Start()
    {
        showCurrentStep();
    }

    void Update()
    {
        OnChangeGear();
        OnChangeHandlebrakeState();
        OnChangeBeltState();

        OnRestartGame();
        ShowHudElements();
    }

    void FixedUpdate()
    {
        MoveCar();
        SteerCar();
        BrakeCar();
    }

    void OnCollisionEnter(Collision colider)
    {
        OnHitColliderLose (colider);
    }

    void OnCollisionStay(Collision colider)
    {
        OnCollisionStayFirstStep (colider);
        OnCollisionStaySecondStep (colider);
    }

    private void MoveCar()
    {
        float motorForce = Math.Abs(Input.GetAxis("Vertical") * motorMaxForce);

        if (moveForward == 0)
        {
            wheelColliderFrontRight.motorTorque = -motorForce;
            wheelColliderFrontLeft.motorTorque = -motorForce;
        }
        if (moveForward == 2)
        {
            if (
                !isHandlebrakeUp &&
                !isBeltFastened &&
                wheelColliderFrontRight.motorTorque > 0
            )
            {
                lose();
                return;
            }
            wheelColliderFrontRight.motorTorque = motorForce;
            wheelColliderFrontLeft.motorTorque = motorForce;
        }
    }

    private void SteerCar()
    {
        float steerForce = Input.GetAxis("Horizontal") * steerMaxForce;

        wheelColliderFrontRight.steerAngle = steerForce;
        wheelColliderFrontLeft.steerAngle = steerForce;
    }

    private void BrakeCar()
    {
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

    private void OnChangeGear()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (moveForward == 1)
                moveForward = 0;
            else if (moveForward == 2) moveForward = 1;
            gearUI.sprite = gear[moveForward];
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (moveForward == 0)
                moveForward = 1;
            else if (moveForward == 1) moveForward = 2;
            gearUI.sprite = gear[moveForward];
        }
    }

    private void OnChangeHandlebrakeState()
    {
        if (Input.GetKeyDown(KeyCode.Space)) isHandlebrakeUp = !isHandlebrakeUp;

        if (isHandlebrakeUp)
        {
            handlebreakUI.sprite = handlebreak[1];
            wheelColliderFrontRight.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderFrontLeft.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderRearRight.brakeTorque = brakeMaxForce * 4.0f;
            wheelColliderRearLeft.brakeTorque = brakeMaxForce * 4.0f;
        }
        else
        {
            handlebreakUI.sprite = handlebreak[0];
            wheelColliderFrontRight.brakeTorque = 0.0f;
            wheelColliderFrontLeft.brakeTorque = 0.0f;
            wheelColliderRearRight.brakeTorque = 0.0f;
            wheelColliderRearLeft.brakeTorque = 0.0f;
        }
    }

    private void OnChangeBeltState()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) isBeltFastened = !isBeltFastened;

        if (isBeltFastened)
            beltUI.sprite = belt[1];
        else
            beltUI.sprite = belt[0];
    }

    private void OnRestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            showHudElements = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            resultUI.sprite = null;
            Time.timeScale = 1;
        }
    }

    private void OnHitColliderLose(Collision colider)
    {
        if (colider.gameObject.tag == "Lose") lose();
    }

    private void OnCollisionStayFirstStep(Collision colider)
    {
        if (car.velocity.magnitude < 0.01)
        {
            if (firstStepBackWasCollider)
            {
                if (colider.gameObject.tag == "First Step Front")
                {
                    isFirstStep = false;
                    showCurrentStep();
                    firstStepBackWasCollider = false;
                    objectiveUI.sprite = objective[1];
                }
            }
            else
            {
                firstStepBackWasCollider =
                    colider.gameObject.tag == "First Step Back" && isFirstStep;
            }
        }
    }

    private void OnCollisionStaySecondStep(Collision colider)
    {
        if (secondStepBackWasCollider)
        {
            if (colider.gameObject.tag == "Second Step Front") win();
        }
        else
        {
            secondStepBackWasCollider =
                colider.gameObject.tag == "Second Step Back" &&
                !isFirstStep &&
                isHandlebrakeUp;
        }
    }

    private void ShowHudElements()
    {
        if (showHudElements)
        {
            gearUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            handlebreakUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            objectiveUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            beltUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            hintUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            resultUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            gearUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            handlebreakUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            objectiveUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            beltUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            hintUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            resultUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    private void win()
    {
        isFirstStep = true;
        gearUI.sprite = null;
        handlebreakUI.sprite = null;
        objectiveUI.sprite = null;
        beltUI.sprite = null;
        showHudElements = false;
        resultUI.sprite = result[1];
        showCurrentStep();
        Time.timeScale = 0;
        secondStepBackWasCollider = false;
    }

    private void lose()
    {
        isFirstStep = false;
        gearUI.sprite = null;
        handlebreakUI.sprite = null;
        objectiveUI.sprite = null;
        beltUI.sprite = null;
        showHudElements = false;
        resultUI.sprite = result[0];
        showCurrentStep();
        Time.timeScale = 0;
    }

    private void showCurrentStep()
    {
        firstStepFront.SetActive (isFirstStep);
        firstStepBack.SetActive (isFirstStep);
        secondStepFront.SetActive(!isFirstStep);
        secondStepBack.SetActive(!isFirstStep);
    }
}
