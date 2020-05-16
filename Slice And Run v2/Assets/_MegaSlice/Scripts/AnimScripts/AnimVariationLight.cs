using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVariationLight : MonoBehaviour
{
    public Renderer myRenderer;
    public float speed = 8;
    public float amount = 1.5f;
    public float gap = 0; 
    public float basicIntensity = 0.1f;
    public int numberMat = 0;

    float valueSin; 
    Color myColor;

    private void Start()
    {
        myColor =  myRenderer.materials[numberMat].GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (myRenderer != null)
        {
            valueSin = Mathf.Sin(Time.time / 2 * speed + gap);
            valueSin += basicIntensity; 
            myRenderer.materials[numberMat].SetColor("_EmissionColor", myColor * valueSin * amount);
        }
    }
}
