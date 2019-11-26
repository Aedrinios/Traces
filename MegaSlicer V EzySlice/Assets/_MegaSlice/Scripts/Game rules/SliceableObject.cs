using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceableObject : MonoBehaviour
{
    private float timeLeft;
    private bool isSliceable;
    public Material crossMaterial;
	float limitVolume = 0.2f;
	float limitNumberCutting = 5; 
	float numberCutting = 0;
	float volume; 

	public void Start()
    {
		InitSliceableObject();        		
    }

    public virtual void Slice(Transform slicePlane)
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
        Rigidbody rb = go.AddComponent<Rigidbody>();
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        SliceableObject so = go.AddComponent<SliceableObject>();
		so.numberCutting = this.numberCutting++;  
        so.crossMaterial = crossMaterial;
        Vector3 explosion = this.transform.position;
		float inverseVolume = 1 / volume;
		inverseVolume = Mathf.Clamp(inverseVolume, 0.001f, 1.4f); 
        rb.AddExplosionForce(300 * inverseVolume, explosion, 20);
    }

    public SlicedHull SliceObject(Transform slicePlane, GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(slicePlane.position, slicePlane.up, crossSectionMaterial);
    }

	void InitSliceableObject()
	{
		isSliceable = false;
		timeLeft = 0.1f;
		Vector3 mySize = GetComponent<Collider>().bounds.size;
		volume = mySize.x * mySize.y * mySize.z; 		

		if (volume <= limitVolume * 0.05f)
		{
			Destroy(gameObject); 
		}
		else if (volume > limitVolume && numberCutting < limitNumberCutting)
		{
			Invoke("ResetIsSliceable", timeLeft); 
		}
	}

	void ResetIsSliceable()
	{
		gameObject.tag = "Sliceable";
		isSliceable = true;
	}
}
