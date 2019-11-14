using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CutSystem;
using EzySlice;
public class PlayerAttack : MonoBehaviour
{
    public LayerMask layerMask;
    public Camera playerCamera;
    public GameObject prefabSlice;
    public Transform cutPlane;
    public Material crossMaterial;

    [HideInInspector] public Vector3 cut;
    public float angle;

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
        Vector3 positionInstance = transform.position;
        positionInstance.y = transform.position.y + 0.8f;
        Instantiate(prefabSlice, positionInstance, cutPlane.rotation);
    }
}
