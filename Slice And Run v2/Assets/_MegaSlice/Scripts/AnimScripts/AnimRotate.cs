using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotate : MonoBehaviour
{
    public Vector3 axeRotation = Vector3.up; 
    public float speedRotate = 100; 

    private void Update()
    {
        transform.Rotate(axeRotation.normalized * speedRotate * Time.deltaTime); 
    }
}
