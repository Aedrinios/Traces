using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutSystem; 

public class Slice : MonoBehaviour
{    
    public Material capMaterial;
    public Vector3 cut;
    public static float angle; 

    bool canCut = true;

    private void Start()
    {
        Cursor.visible = false;    
    }

    private void Update()
    {
        CalculateAxeMouse();
        ReturnAngle();

        if (Input.GetButtonDown("Fire1") && canCut)
        {
            Cut(); 
        }
    }

    void CalculateAxeMouse()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 inputCut = mousePosition - screenCenter;   
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

    void Cut()
    {
        //Vector3 realCut = cut; 


        Vector3 realCut = new Vector3(cut.y, -cut.x, 0);

        GameObject[] newObjects = MeshCut.Cut(gameObject, transform.position, realCut, capMaterial);
        canCut = false;

        for (int i = 0; i < 2; i++)
        {
            newObjects[i].AddComponent<BoxCollider>();
            newObjects[i].AddComponent<Rigidbody>();
            newObjects[i].GetComponent<Rigidbody>().mass = 10; 
        }
    }
}
