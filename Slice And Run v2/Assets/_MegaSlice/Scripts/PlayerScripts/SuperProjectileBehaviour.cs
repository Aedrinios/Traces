using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SuperProjectileBehaviour : ProjectileBehaviour
{
    [SerializeField] private Material cutMaterial;

    //public override void Slice(GameObject hit)
    //{
    //    SlicedHull hull = SliceObject(cutPlane, hit, cutMaterial);

    //    if (hull != null)
    //    {
    //        GameObject bottom = hull.CreateLowerHull(this.gameObject, cutMaterial);
    //        GameObject top = hull.CreateUpperHull(this.gameObject, cutMaterial);
    //        AddHullComponents(bottom);
    //        AddHullComponents(top);
    //    }
    //}

    public void AddHullComponents(GameObject go)
    {
        Rigidbody rb = go.AddComponent<Rigidbody>();
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        RepulsionAfterCut(rb);
    }

    public void RepulsionAfterCut(Rigidbody rb)
    {
        Vector3 posPlayer = FPS_Controller.playerPos;
        rb.AddExplosionForce(80 * 10, posPlayer, 800);
    }

    public SlicedHull SliceObject(Transform slicePlane, GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(slicePlane.position, slicePlane.up, crossSectionMaterial);
    }

    //public override void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Player"))
    //    {
    //        Slice(other.gameObject);
    //    }
    //}

}
