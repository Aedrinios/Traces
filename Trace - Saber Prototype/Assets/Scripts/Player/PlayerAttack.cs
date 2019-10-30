using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutSystem;

public class PlayerAttack : MonoBehaviour
{
    public Material capMaterial;

    public Vector3 cut;
    public static float angle;
    public static float wheelAngle;
    DetectObjectInFront detectObjectInFront;

    private void Start()
    {
        detectObjectInFront = transform.GetChild(1).GetComponent<DetectObjectInFront>();
        Cursor.visible = false;
    }

    public void Update()
    {
        CalculateAxeMouse();
        ReturnAngle();
        if (Input.GetButtonDown("Fire1"))
        {
            Cut();
        }
    }

    void Cut()
    {
        Vector3 realCut = new Vector3(cut.y, -cut.x, 0);
        
    
        {
            for (int i = 0; i < detectObjectInFront.listNearSliceable.Count; i++)
            {
                GameObject[] newObjects = MeshCut.Cut(detectObjectInFront.listNearSliceable[i], transform.position, realCut, capMaterial);

                for (int j = 0; j < 2; j++)
                {
                    newObjects[j].AddComponent<BoxCollider>();
                    newObjects[j].AddComponent<Rigidbody>();
                    newObjects[j].GetComponent<Rigidbody>().mass = 10;
                    newObjects[j].gameObject.tag = "Sliceable";
                }
            }
        }
    }

    void CalculateAxeMouse()
    {
        // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //Vector3 inputCut = mousePosition - screenCenter;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 inputCut = new Vector3(mouseX, mouseY, 0);  
        inputCut = inputCut.normalized;

        if (inputCut != Vector3.zero)
        {
            cut = inputCut;
        }
    }

    void ReturnAngle()
    {
        Vector2 cut_2D = new Vector2(cut.x, cut.y);

        cut_2D = cut_2D.normalized;
        angle = Vector2.Angle(-Vector2.right, cut_2D);
        if (cut_2D.y > 0) angle = -angle;
    }


    /*public void Slicing()
    {
        if(objectInFront != null)
        {
            float directionComparison = Mathf.Abs(Vector3.Dot(GetComponent<MouseDirection>().direction, objectInFront.WantedDirection));
            if (directionComparison > 0.95f)
            {
                objectInFront.OnDestroy();
            }
        }
    }*/
}
