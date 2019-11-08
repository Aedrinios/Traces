﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
//[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class SliceableObject : MonoBehaviour
{
    private float timeLeft;
    private bool isSliceable;
    public GameObject player;
    public Material crossMaterial;

    void Start()
    {
        isSliceable = false;
        timeLeft = 0.1f;
        gameObject.layer = LayerMask.NameToLayer("Sliceable");        
        player = GameObject.FindWithTag("Player");
        Debug.Log("Time : " + timeLeft);
      //  Vector3 expulsion = transform.position - player.transform.position;
      //  float push = player.GetComponent<PlayerAttack>().forcePush;
      //  GetComponent<Rigidbody>().AddForce(expulsion * push);
    }

    public void Update()
    { 
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            isSliceable = true;
}
    }

    public void Slice(Transform slicePlane)
    {
        if (isSliceable)
        {
            SlicedHull hull = SliceObject(slicePlane, this.gameObject, crossMaterial);
            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(this.gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(this.gameObject, crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
                Destroy(this.gameObject);
            }
        }
    }

    public void AddHullComponents(GameObject go)
    {
        go.tag = "Sliceable";
        Rigidbody rb = go.AddComponent<Rigidbody>();
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        SliceableObject so = go.AddComponent<SliceableObject>();
        so.crossMaterial = crossMaterial;
        Vector3 positionToPlayer = transform.position - player.transform.position;
        Vector3 explosion = new Vector3(positionToPlayer.x + Random.Range(0, 10), positionToPlayer.y + Random.Range(0, 10), positionToPlayer.z);
        rb.AddExplosionForce(1000, explosion, 20);
    }

    public SlicedHull SliceObject(Transform slicePlane, GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(slicePlane.position, slicePlane.up, crossSectionMaterial);
    }
}
