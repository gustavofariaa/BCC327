using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color initialColor = Color.green;
    public bool changeAlpha = false;
    public float timeToChange = 1;
    public float speedChange = 5;

    private Material objectMaterial;
    private Color randomColor;
    private float timer;

    void Start()
    {
        objectMaterial = GetComponent<MeshRenderer>().material;
        objectMaterial.color = initialColor;
        timer = 0;
        ChangeColor();
    }

    void Update()
    {
        objectMaterial.color = Color.Lerp(objectMaterial.color, randomColor, Time.deltaTime * speedChange);

        timer += Time.deltaTime;
        if (timer > timeToChange)
        {
            timer = 0;
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        var red = Random.Range(0.0f, 1.0f);
        var blue = Random.Range(0.0f, 1.0f);
        var green = Random.Range(0.0f, 1.0f);
        var alpha = 1.0f;

        if (changeAlpha)
        {
            alpha = Random.Range(0.0f, 1.0f);
        }

        randomColor = new Color(red, blue, green, alpha);
    }
}
