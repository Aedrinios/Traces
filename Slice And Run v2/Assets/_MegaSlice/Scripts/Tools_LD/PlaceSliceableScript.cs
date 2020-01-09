using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceSliceableScript : MonoBehaviour
{
	//pratique pour mettre en place rapidement les objets coupé d'une scène
	private void OnEnable()
	{
		gameObject.tag = "Sliceable";

		if (!GetComponent<Collider>())
		{
			gameObject.AddComponent<MeshCollider>();
			GetComponent<MeshCollider>().convex = true; 
		}

		if (!GetComponent<SliceableObject>())
		{
			gameObject.AddComponent<SliceableObject>();
		}

		DestroyImmediate(this);

	}
}
