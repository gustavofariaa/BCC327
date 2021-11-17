using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public new Transform camera;

    private bool stopTranslate = true;

    void Update()
    {
        if (stopTranslate)
        {
            transform.LookAt(camera);
            transform.Translate(-5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stopTranslate = !stopTranslate;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(40 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 5 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 5, 5), Time.deltaTime);
        }

    }
}
