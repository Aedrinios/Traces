using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableEnemy : SliceableObject
{
    [SerializeField] private float requiredAngle;
    [SerializeField] private float tolerance;
    [SerializeField] private int armorCounter;
    private bool isArmorOn = true;

    public override void Slice(Transform slicePlane)
    {
        if(Mathf.Abs(MouseControl.angle - requiredAngle) < tolerance || armorCounter <= 0)
        {
            base.Slice(slicePlane);
        }
        else
        {
            armorCounter--;
        }
    }
}
