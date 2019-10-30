using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutSystem;

public class PlayerAttack : MonoBehaviour
{
    public float range = 2;
    public float radius = 10;
    public float forcePush = 300; 
    public LayerMask layer; 
    public Material capMaterial;
    public Camera playerCamera;

    public GameObject slicePlane;

    [HideInInspector] public Vector3 cut;
    public static float angle;
    //public DetectObjectInFront detectObjectInFront;

    // c'est le point où l'objet va se couper
    Vector3 anchorCut;
    Collider[] nearSliceable; 

    private void Start()
    {
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
         RaycastHit hit;
         Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, layer);
         if (hit.collider != null)
         {
             anchorCut = hit.point;
             nearSliceable = Physics.OverlapSphere(anchorCut, radius, layer);
         }

        //anchorCut = slicePlane.transform.position;

        // découpe les objets
        //Vector3 realCut = new Vector3(cut.y, -cut.x, 0);        
        Vector3 realCut = new Vector3(-cut.y, cut.x, 0); 


        if (nearSliceable != null && nearSliceable.Length != 0)
        {
            for (int i = 0; i < nearSliceable.Length; i++)
            {
                GameObject cutObject = nearSliceable[i].gameObject; 
                GameObject[] newObjects = MeshCut.Cut(cutObject, anchorCut, realCut, capMaterial);

                for (int j = 0; j < 2; j++)
                {
                    newObjects[j].AddComponent<SliceableObject>();                              
                }
                Destroy(cutObject); 
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
}
