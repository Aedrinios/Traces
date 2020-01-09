using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	// variable utilisé pour régler les métrics du jeu depuis l'éditeur
	public float actualScore = 0;
	public float maximumScore = 1000;
	public float losePerSec = 1; 

	// variable static utilisée pour les donnés du jeu
	public static float maxScore; 
	public static float score;
	

	private void Awake()
	{
		score = 0; 
		maxScore = maximumScore; 
	}

	private void Update()
	{
		actualScore = score;
		score -= Time.deltaTime * losePerSec; 
		score = Mathf.Clamp(score, 0, maxScore);
	}

	public static void GainScore(int gain)
	{
		score += gain;
	}
}
