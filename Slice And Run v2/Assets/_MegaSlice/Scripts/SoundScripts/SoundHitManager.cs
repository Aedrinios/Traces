using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHitManager : MonoBehaviour
{
	public GameObject hitSound;
	public float ratioVolume = 1;
	public float minTime = 0.01f; 
	private float volume = 1; 

	Vector3 posSpawn;

	float chrono = 0; 

	private void OnEnable()
	{
		SliceableObject.hitHappen += PlaySoundHit; 		
	}

	private void OnDisable ()
	{
		SliceableObject.hitHappen -= PlaySoundHit;
	}

	void PlaySoundHit()
	{
		posSpawn = SliceableObject.positionSlice; 
		GameObject newSound = Instantiate(hitSound, posSpawn, transform.rotation) as GameObject;

		
		float inverseChrono = 1 / Mathf.Exp(chrono);
		volume = 1 - (inverseChrono * ratioVolume); 
		volume = Mathf.Clamp(volume, 0, 1); 


		chrono = 0; 
	}

	private void FixedUpdate()
	{
		chrono += Time.deltaTime; 
	}

}
