//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MouseControl : MonoBehaviour
//{
//    public static float angle;
//    public static Vector3 inputCut { get; private set; }

//    [SerializeField] private float mouseSensitivity;

//    private void Update()
//    {
//        CalculateAxeMouse();
//    }

//    void CalculateAxeMouse()
//    {
//        float mouseX = Input.GetAxis("Mouse X");
//        float mouseY = Input.GetAxis("Mouse Y");

//        Vector3 inputMouse = (new Vector3(mouseX, mouseY));
//        if(inputMouse.magnitude >= mouseSensitivity)
//        {
//            inputCut = inputMouse.normalized;
//        }

//        if (inputCut != Vector3.zero)
//        {
//            Vector2 cut_2D = new Vector2(inputCut.x, inputCut.y);
//            angle = Vector2.Angle(-Vector2.right, cut_2D);
//            if (cut_2D.y > 0) angle = -angle;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    List<Vector2> oldMousePositions = new List<Vector2>();
    [SerializeField] int maxPoints = 30;
    [SerializeField] float minDistance = 0.1f;
    Vector2 oldMouseDelta;

    [Header("Smooth")]
    [SerializeField, Range(0, 1)] float smooth = 0.1f;
    public static float currentAngle;
    float refAngle;

    Vector2 MousePosition
    {
        get
        {
            return Input.mousePosition;
        }
    }

    void Update()
    {
        Debug.Log("Input mouse : " + Input.mousePosition);
        oldMousePositions.Add(Input.mousePosition);
        if (oldMousePositions.Count > maxPoints)
            oldMousePositions.RemoveAt(0);

        Vector2 mouseDelta;
        int referencePointId = oldMousePositions.Count - 1;
        while (Vector2.Distance(MousePosition, oldMousePositions[referencePointId]) < minDistance)
        {
            referencePointId--;
            if (referencePointId < 0)
                break;
        }

        if (referencePointId < 0)
        {
            mouseDelta = oldMouseDelta;
        }
        else
        {
            mouseDelta = MousePosition - oldMousePositions[referencePointId];
            oldMouseDelta = mouseDelta;
        }

        float targetAngle = -Vector2.SignedAngle(mouseDelta, Vector2.right);
        targetAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref refAngle, smooth);
        //  transform.localEulerAngles = new Vector3(0, 0, targetAngle);
        currentAngle = targetAngle;
    }
}