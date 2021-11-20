using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime, 90 * Time.deltaTime, 30 * Time.deltaTime);
    }
}
