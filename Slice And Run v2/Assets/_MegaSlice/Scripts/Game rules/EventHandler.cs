using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
	// ce script contient tous les évènements du jeu, attention à ne pas le supprimer !!!

	public delegate void Action();
	public static Action cutObject;

	public static Action BeginGame; 

	public static Action GainWallJump;
	public static Action StartPastequeRain;
}

