using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;

    void FixedUpdate()
    {
        transform.LookAt(car.transform);
    }
}
