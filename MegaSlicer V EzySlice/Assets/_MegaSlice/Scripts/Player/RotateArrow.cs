using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RotateArrow : MonoBehaviour
{
    private float rotationZ;

    private void Start()
    {
    }

    private void Update()
    {
        rotationZ = MouseControl.angle;
        transform.localEulerAngles = new Vector3(0, 0, rotationZ);
    }
}
