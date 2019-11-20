using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableEnemy : SliceableObject
{
    [SerializeField] private float requiredAngle;
    [SerializeField] private float tolerance;

    private bool isArmorOn = true;

    public override void Slice(Transform slicePlane)
    {
        Debug.Log("souris : " + MouseControl.angle);
        Debug.Log("requiredAngle : " + requiredAngle);
        Debug.Log("difference : " + Mathf.Abs(MouseControl.angle - requiredAngle));
        if(Mathf.Abs(MouseControl.angle - requiredAngle) < tolerance || !isArmorOn)
        {
            base.Slice(slicePlane);
        }
        else
        {
            isArmorOn = false;
        }
    }
}
