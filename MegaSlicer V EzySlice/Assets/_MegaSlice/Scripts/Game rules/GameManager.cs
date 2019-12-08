using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float forcePushCut = 80; 

	public static float forcePushCutStc;

	private void Awake()
	{
		forcePushCutStc = forcePushCut; 
	}
}
