using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlane : MonoBehaviour
{
    public float rotationZ = -90;
    public float lerp;

    private void Update()
    {
        rotationZ = PlayerAttack.angle;
        transform.localEulerAngles = new Vector3(0, 0, rotationZ);
    }
}
