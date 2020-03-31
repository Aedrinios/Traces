using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RotateArrow : MonoBehaviour
{
    public static bool canRotate = true;

    public float decalAngle; 
    public float rotationZ;

    private void Update()
    {
        if (canRotate)
        {
            rotationZ = MouseControl.angle;
            transform.localEulerAngles = new Vector3(0, 0, rotationZ + decalAngle);
        }
    }
}
