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
    public GameObject prefabSlice;
    public Transform cutPlane;
    public Material crossMaterial;

    [HideInInspector] public Vector3 cut;
    public float angle;
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
        if (Input.GetButtonDown("Fire1"))
        {
            LaunchProjectile();
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
        Instantiate(prefabSlice, transform.position,Quaternion.Euler(MouseControl.inputCut));

        nearSliceable = Physics.OverlapBox(cutPlane.position, new Vector3(10, 0.1f, 10), cutPlane.rotation, layerMask);
        if (nearSliceable.Length <= 0)
            return;

        for (int i = 0; i < nearSliceable.Length; i++)
        {
            nearSliceable[i].gameObject.GetComponent<SliceableObject>().Slice(cutPlane);
        }
    }

    public void LaunchProjectile()
    {
        //Vector3 instantiatePosition = new Vector3(Screen.width / 2, Screen.height / 2, 1);
        Vector3 positionInstance = transform.position;
        positionInstance.y = transform.position.y + 1.2f;
        Instantiate(prefabSlice, positionInstance, cutPlane.rotation);

        /*
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
        }*/
    }
}
