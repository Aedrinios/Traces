using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [Header("Parameter rotation nul")]
    public static float angle;
    public static Vector3 inputCut { get; private set; }
    public float mouseSensitivity;

    [Header("Parameter rotation collot")]
    public List<Vector2> oldMousePositions = new List<Vector2>();
    [SerializeField] int maxPoints = 30;
    [SerializeField] float minDistance = 0.1f;
    Vector2 oldMouseDelta;

    [Header("Smooth")]
    [SerializeField, Range(0, 1)] float smooth = 0.1f;
    public static float currentAngle;
    float refAngle;

    public bool collotRotation;

    Vector2 MousePosition
    {
        get
        {
            return Input.mousePosition;
            //float mouseX = Input.GetAxisRaw("Mouse X");
            //float mouseY = Input.GetAxisRaw("Mouse Y");
            //return new Vector2(mouseX, mouseY);
        }
    }

    void Update()
    {
        if (collotRotation)
        {
            oldMousePositions.Add(MousePosition);
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
        else
        {
            CalculateAxeMouse();
        }
    }

    void CalculateAxeMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 inputMouse = (new Vector3(mouseX, mouseY));
        if (inputMouse.magnitude >= mouseSensitivity)
        {
            inputCut = inputMouse.normalized;
        }

        if (inputCut != Vector3.zero)
        {
            Vector2 cut_2D = new Vector2(inputCut.x, inputCut.y);
            currentAngle = Vector2.SignedAngle(-Vector2.right, cut_2D);
        }
    }
}