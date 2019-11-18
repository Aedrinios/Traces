using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceableObject : MonoBehaviour
{
	public Material crossMaterial;
	private float timeLeft = 0.1f;
	public float explosionForce = 300; 
	private bool isSliceable;

	Vector3 originPos; 

	void Start()
    {
        isSliceable = false;
        gameObject.layer = LayerMask.NameToLayer("Sliceable");
		Invoke("ResetIsSliceable", timeLeft);

	}

    public void Slice(Transform slicePlane)
    {
        if (isSliceable)
        {
			originPos = transform.position; 
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
		if (GetComponent<Rigidbody>())
		{
			rb.mass = GetComponent<Rigidbody>().mass;
		}

        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        SliceableObject so = go.AddComponent<SliceableObject>();
        so.crossMaterial = crossMaterial;

		//Vector3 explosion = new Vector3(positionToPlayer.x + Random.Range(0, 10), positionToPlayer.y + Random.Range(0, 10), positionToPlayer.z);

		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		//Vector3 dirExplose = originPos - player.transform.position;
		//Debug.Log(dirExplose); 
		//rb.AddForce(dirExplose.normalized * explosionForce);

		rb.AddExplosionForce(explosionForce, originPos, 10); 
		
    }

    public SlicedHull SliceObject(Transform slicePlane, GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(slicePlane.position, slicePlane.up, crossSectionMaterial);
    }

	void ResetIsSliceable()
	{
		isSliceable = true; 
	}
}
