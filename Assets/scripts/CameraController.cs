using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;
    private float sensibility = 2.0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * sensibility;
        mouseY -= Input.GetAxis("Mouse Y") * sensibility;
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
    }
}
