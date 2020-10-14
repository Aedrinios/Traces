using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public static float currentAngle;

    [Header("Parameter rotation mouse")]
    public bool oldRotation = false; 
    public float maxSensivity = 2.5f;
    public float minSensivity = 0.1f;
    public float frameRefesh = 1;
    Vector3 inputMouse;
    float chrono = 0; 

    [Header("Parameter rotation collot")]
    public List<Vector2> oldMouseMovement = new List<Vector2>();
    [SerializeField] int maxPoints = 3;
    [SerializeField] float minDistance = 0.1f;
    Vector2 oldMouseDelta;
    [SerializeField, Range(0, 1)] float smooth = 0.1f;
    float refAngle;
    public bool collotRotation;

    [Header("Parameter rotation average")]
    public bool averageRotation;
    [SerializeField] int maxAveragePoints = 3;
    Vector2 MouseMovement { get { return new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); } }
    Vector2 averageMouseMovement;

    void Update()
    {
        if (collotRotation)
        {
            CollotCalculateRotation(); 
        }
        else if (oldRotation)
        {
            CalculateAxeMouse();
        }
        else if (averageRotation)
        {
            AverageAxeMouse();
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
        oldMouseMovement.Add(MouseMovement);
        if (oldMouseMovement.Count > maxPoints)
            oldMouseMovement.RemoveAt(0);

        Vector2 mouseDelta;
        int referencePointId = oldMouseMovement.Count - 1;
        while (Vector2.Distance(MouseMovement, oldMouseMovement[referencePointId]) < minDistance)
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
            mouseDelta = MouseMovement - oldMouseMovement[referencePointId];
            oldMouseDelta = mouseDelta;
        }

        float targetAngle = -Vector2.SignedAngle(mouseDelta, Vector2.right);
        targetAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref refAngle, smooth);
        //  transform.localEulerAngles = new Vector3(0, 0, targetAngle);
        currentAngle = targetAngle;
    }

    void AverageAxeMouse()
    {
        averageMouseMovement = Vector2.zero;
        if(MouseMovement != Vector2.zero)
            oldMouseMovement.Add(MouseMovement);

        if (oldMouseMovement.Count > maxAveragePoints)
            oldMouseMovement.RemoveAt(0);

        for(int i = 0; i < oldMouseMovement.Count; i++)
        {
            averageMouseMovement += oldMouseMovement[i];
        }

        //Debug.Log(averageMouseMovement); 

        //if (averageMouseMovement.magnitude >= minSensivity && averageMouseMovement.magnitude < maxSensivity)
        //{
            float newAngle = -Vector2.SignedAngle(averageMouseMovement, Vector2.right);
            currentAngle = newAngle;
        //}
    }
}