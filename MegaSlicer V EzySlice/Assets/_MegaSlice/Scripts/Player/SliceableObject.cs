using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
//[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class SliceableObject : MonoBehaviour
{
    public GameObject player;
    public Material crossMaterial;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Sliceable");        
        GetComponent<MeshCollider>().convex = true;
        player = GameObject.FindWithTag("Player");
      //  Vector3 expulsion = transform.position - player.transform.position;
      //  float push = player.GetComponent<PlayerAttack>().forcePush;
      //  GetComponent<Rigidbody>().AddForce(expulsion * push);
    }

    public void Slice(Transform slicePlane)
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

    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
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
