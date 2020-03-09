using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSoundManager : MonoBehaviour
{
	public GameObject soundDie;  

	private void OnEnable()
	{
		BasicTools.resetEvent += PlaySoundDie;
	}

	private void OnDisable()
	{
		BasicTools.resetEvent -= PlaySoundDie;
	}

	void PlaySoundDie()
	{
		Instantiate(soundDie, transform.position, transform.rotation); 
	}
}
