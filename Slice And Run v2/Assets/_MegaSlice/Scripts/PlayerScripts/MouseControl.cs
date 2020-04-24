using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float maxSensivity = 2.5f;
    public float minSensivity = 0.1f;
    public float frameRefesh = 1;
    //public float minAngleChange = 5; 

    Vector3 inputMouse;
    float chrono = 0; 

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
    Vector2 MousePosition { get { return Input.mousePosition; } }

    void Update()
    {
        if (collotRotation)
        {
            CollotCalculateRotation(); 
        }
        else
        {
            CalculateAxeMouse();
        }
    }

    void CalculateAxeMouse()
    {
        chrono += Time.deltaTime; 
        if (chrono >= Time.deltaTime * frameRefesh)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            inputMouse = new Vector3(mouseX, mouseY, 0);
            chrono = 0;
        }

        if (inputMouse.magnitude >= minSensivity && inputMouse.magnitude < maxSensivity)
        {
            float newAngle = -Vector2.SignedAngle(inputMouse, Vector2.right);
            currentAngle = newAngle;
        }
    } 


    float InverseAngle(float angle)
    {
        Vector3 inverseVector = Quaternion.AngleAxis(angle, Vector3.forward) * -Vector3.right;
        float inverse = -Vector2.SignedAngle(inverseVector, Vector2.right);
        return inverse;
    }

    void CollotCalculateRotation()
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
}