using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVariationLight : MonoBehaviour
{
    public Renderer myRenderer;
    public AnimationCurve curve;

    public float speed = 8;
    public float amount = 1.5f;
    public float gap = 0; 
    public float basicIntensity = 0.1f;
    public int numberMat = 0; 

    float ratioCurve;
    Color myColor;

    private void Start()
    {
        myColor =  myRenderer.materials[numberMat].GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (myRenderer != null)
        {
            ratioCurve = curve.Evaluate((Time.time / 10) * speed + gap);
            ratioCurve += basicIntensity;
            myRenderer.materials[numberMat].SetColor("_EmissionColor", myColor * ratioCurve * amount);
        }
    }
}
