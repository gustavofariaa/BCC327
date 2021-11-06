using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipRearMirror : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        mainCamera.projectionMatrix = mainCamera.projectionMatrix * Matrix4x4.Scale(new Vector3 (-1, 1, 1));
    }

    void Update()
    {
        
    }
}
