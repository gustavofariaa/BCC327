using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Text textGetButtonDown;
    public Text textGetButton;
    public Text textGetButtonUp;

    void Update()
    {
        bool isGetButtonDown = Input.GetButtonDown("Jump");
        bool isGetButton = Input.GetButton("Jump");
        bool isGetButtonUp = Input.GetButtonUp("Jump");

        textGetButtonDown.text = "GetButtonDown = " + isGetButtonDown;
        textGetButton.text = "GetButton = " + isGetButton;
        textGetButtonUp.text = "GetButtonUp = " + isGetButtonUp;
    }
}
