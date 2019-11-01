using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    [SerializeField] [Range(0, 100) ]private float speed;
    public Transform cutPlane;
    public LayerMask layerMask;
    public Material crossMaterial;

    private void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Slice(GameObject hit)
    {
        //Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hit == null)
        {
            return;
        }

        SlicedHull hull = SliceObject(hit, crossMaterial);
        if (hull != null)
        {
            GameObject bottom = hull.CreateLowerHull(hit, crossMaterial);
            GameObject top = hull.CreateUpperHull(hit, crossMaterial);
            AddHullComponents(bottom);
            AddHullComponents(top);
            Destroy(hit.gameObject);
            
        }
    }

    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        go.tag = "Sliceable";
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            Slice(other.gameObject);
        }
    }
}
