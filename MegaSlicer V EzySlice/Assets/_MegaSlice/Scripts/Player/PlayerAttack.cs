using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CutSystem;
using EzySlice;
public class PlayerAttack : MonoBehaviour
{
  //  public float range = 2;
  //  public float radius = 10;
   // public float forcePush = 300;
    public LayerMask layerMask;
    public Camera playerCamera;

    public Transform cutPlane;
    public Material crossMaterial;

    [HideInInspector] public Vector3 cut;
    public static float angle;
    public float mouseSensitivity;
    //public DetectObjectInFront detectObjectInFront;

    // c'est le point où l'objet va se couper
    Vector3 anchorCut;
    public Collider[] nearSliceable;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        CalculateAxeMouse();
        ReturnAngle();
        if (Input.GetButtonDown("Fire1"))
        {
            Slice();
        }
    }

    void Cut()
    {

       /* RaycastHit hit;
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
        }*/
        nearSliceable = Physics.OverlapBox(cutPlane.position, new Vector3(10, 0.1f, 10), cutPlane.rotation, layerMask);
        if (nearSliceable.Length <= 0)
            return;

        for (int i = 0; i < nearSliceable.Length; i++)
        {
            nearSliceable[i].gameObject.GetComponent<SliceableObject>().Slice(cutPlane);
        }
    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
        {
            return;

        }

        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
                Destroy(hits[i].gameObject);
            }
        }
    }

    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 20);
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
        {
            return null;
        }

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }
    
    void CalculateAxeMouse()
    {

        // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //Vector3 inputCut = mousePosition - screenCenter;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 inputCut = Vector3.zero;


        if(mouseX < -mouseSensitivity || mouseX > mouseSensitivity)
        {
            inputCut.x = mouseX;
        }
        if (mouseY < -mouseSensitivity || mouseY > mouseSensitivity)
        {
            inputCut.y = mouseY;
        }
        
        inputCut = inputCut.normalized;
        Debug.Log("inputCut : " + inputCut);    
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
