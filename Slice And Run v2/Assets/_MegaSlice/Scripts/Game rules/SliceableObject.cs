using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

[RequireComponent(typeof(ReactionAtSlice))]
public class SliceableObject : MonoBehaviour
{
    private float timeLeft;
    private bool isSliceable;
    public Material crossMaterial;
	float limitNumberCutting = 15; 

	[HideInInspector] public float numberCutting = 0;
	[HideInInspector] public float forcePush = 80;
	ReactionAtSlice reaction; 

	public void Start()
    {
		InitSliceableObject();
		reaction = GetComponent<ReactionAtSlice>(); 
		forcePush = GameManager.forcePushCutStc; 		
	}

    public virtual void Slice(Transform slicePlane)
    {
        if (isSliceable)
        {
			DetachObjectChild(); 
            SlicedHull hull = SliceObject(slicePlane, this.gameObject, crossMaterial);

            if (hull != null)
            {
				//slice this item
				EventHandler.cutObject?.Invoke();				 
				GameObject bottom = hull.CreateLowerHull(this.gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(this.gameObject, crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
				//reaction at slice
				if (reaction != null) reaction.allReactionAtSlice();
				//GainBonusTime();

				//end of slice
				Destroy(this.gameObject);
            }
        }
    }

    public void AddHullComponents(GameObject go)
    {
        Rigidbody rb = go.AddComponent<Rigidbody>();
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
		if (numberCutting < limitNumberCutting - 1)
		{
			collider.material = this.GetComponent<Collider>().material;
		}
        SliceableObject so = go.AddComponent<SliceableObject>();
		so.numberCutting = this.numberCutting++;  
        so.crossMaterial = crossMaterial;
		RepulsionAfterCut(rb); 
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

		if ( numberCutting < limitNumberCutting)
		{
			Invoke("ResetIsSliceable", timeLeft); 
		}
	}

	void ResetIsSliceable()
	{
		gameObject.tag = "Sliceable";
		isSliceable = true;
	}

	void DetachObjectChild()
	{
		transform.parent = null; 
		Transform[] children = GetComponentsInChildren<Transform>();
		for (int i = 1; i < children.Length; i++)
		{
			children[i].parent = null; 
		}
	}

	public void GainBonusTime()
	{
		if (numberCutting == 0)
		{
			LifeTimerManager.lifeTimer += LifeTimerManager.multiplierBonusCutStatic;
		}
		else if (numberCutting == 1)
		{
			LifeTimerManager.lifeTimer += 0.25f * LifeTimerManager.multiplierBonusCutStatic; 
		}
		else if (numberCutting == 2)
		{
			LifeTimerManager.lifeTimer += 0.05f * LifeTimerManager.multiplierBonusCutStatic; 
		}
		else
		{
			LifeTimerManager.lifeTimer += 0.01f * LifeTimerManager.multiplierBonusCutStatic; 
		}
	}


	public void RepulsionAfterCut(Rigidbody rb)
	{
		Vector3 posPlayer = FPS_Controller.playerPos;
		rb.AddExplosionForce(forcePush * 10, posPlayer, 800); 
		
	}
}
