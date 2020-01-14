using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RotateArrow : MonoBehaviour
{
    public Vector3 decalAngle = Vector3.zero; 
    private float rotationZ;

    private void Update()
    {
        rotationZ = MouseControl.angle;
        transform.localEulerAngles = new Vector3(0, 0, rotationZ) + decalAngle;
    }
}
