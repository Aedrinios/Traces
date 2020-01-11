using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceableObject : MonoBehaviour
{
    private float timeLeft;
    private bool isSliceable;
    public Material crossMaterial;
	public GameObject hitSound; 
	float bonusTime = 1;

	float volume;
	float limitVolume = 0.25f;
	float ratioDestroy = 0.15f; 
	float limitNumberCutting = 15; 

	[HideInInspector] public float numberCutting = 0;
	[HideInInspector] public float forcePush = 80;

	public void Start()
    {
		InitSliceableObject();
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
				EventHandler.cutObject?.Invoke();
				GainBonusTime(); 
				GameObject bottom = hull.CreateLowerHull(this.gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(this.gameObject, crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
				if (hitSound != null) 
				{
					Instantiate(hitSound, transform.position, transform.rotation);
				}
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

		//ExplosionAfterCut(rb); 
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
		Vector3 mySize = GetComponent<Collider>().bounds.size;
		volume = mySize.x * mySize.y * mySize.z; 		

		if (volume <= limitVolume * ratioDestroy)
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
			LifeTimerManager.lifeTimer += bonusTime * LifeTimerManager.multiplierBonusCutStatic;
		}
		else if (numberCutting == 1)
		{
			LifeTimerManager.lifeTimer += bonusTime * 0.25f * LifeTimerManager.multiplierBonusCutStatic; 
		}
		else if (numberCutting == 2)
		{
			LifeTimerManager.lifeTimer += bonusTime * 0.05f * LifeTimerManager.multiplierBonusCutStatic; 
		}
		else
		{
			LifeTimerManager.lifeTimer += bonusTime * 0.01f * LifeTimerManager.multiplierBonusCutStatic; 
		}
	}

	public void ExplosionAfterCut(Rigidbody rb)
	{
		Vector3 explosion = this.transform.position;
		float inverseVolume = 1 / volume;
		inverseVolume = Mathf.Clamp(inverseVolume, 0.001f, 1.8f);
		rb.AddExplosionForce(forcePush * 10 * inverseVolume, explosion, 20);
	}

	public void RepulsionAfterCut(Rigidbody rb)
	{
		Vector3 posPlayer = FPS_Controller.playerPos;
		rb.AddExplosionForce(forcePush * 10, posPlayer, 800); 
		
	}
}
