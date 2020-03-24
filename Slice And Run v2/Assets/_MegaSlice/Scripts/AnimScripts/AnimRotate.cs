using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotate : MonoBehaviour
{
    public Vector3 axeRotation = Vector3.up; 
    public float speedRotate = 10; 

    private void FixedUpdate()
    {
        transform.Rotate(axeRotation.normalized * speedRotate * Time.deltaTime); 
    }
}
