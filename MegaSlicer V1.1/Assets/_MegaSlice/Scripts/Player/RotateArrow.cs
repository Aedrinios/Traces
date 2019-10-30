using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RotateArrow : MonoBehaviour
{
    public float rotationZ = -90;
    public float lerp;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rotationZ = Mathf.Lerp(rotationZ, PlayerAttack.angle, lerp);
        rectTransform.localEulerAngles = new Vector3(0, 0, rotationZ);
    }
}
