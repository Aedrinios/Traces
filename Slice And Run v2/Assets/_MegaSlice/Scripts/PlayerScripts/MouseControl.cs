using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public static float angle;
    public static Vector3 inputCut { get; private set; }

    [SerializeField] private float mouseSensitivity;

    private void Update()
    {
        CalculateAxeMouse();
    }

    void CalculateAxeMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 inputMouse = (new Vector3(mouseX, mouseY));
        if(inputMouse.magnitude >= mouseSensitivity)
        {
            inputCut = inputMouse.normalized;
        }

        if (inputCut != Vector3.zero)
        {
            Vector2 cut_2D = new Vector2(inputCut.x, inputCut.y);
            angle = Vector2.Angle(-Vector2.right, cut_2D);
            if (cut_2D.y > 0) angle = -angle;
        }
    }
}
