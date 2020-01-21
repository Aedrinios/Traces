using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SliceableObject))]
public class ReactionAtSlice : MonoBehaviour
{
	public float numberCutting;
	float powerVolume = 1.2f; 

	private void Start()
	{
		powerVolume = LifeTimerManager.powerVolumeStc; 
		numberCutting = GetComponent<SliceableObject>().numberCutting; 
	}

	public void GainBonusTime()
	{
		//GainConstantBonusTime();
		GainBonusTimeByVolume(); 
	}

	void GainConstantBonusTime()
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

	void GainBonusTimeByVolume()
	{
		float volume = 0;
		Vector3 size = GetComponent<MeshFilter>().mesh.bounds.size;
		Vector3 myScale = transform.localScale;
		volume = size.x * myScale.x * size.y * myScale.y * size.z * myScale.z;

		float bonusTime = Mathf.Pow(volume, powerVolume);
		bonusTime *= LifeTimerManager.multiplierBonusCutStatic;

		LifeTimerManager.lifeTimer += bonusTime; 
	}


}
