using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScore : MonoBehaviour
{
	public float firstStep;
	public float secondStep;

	bool firstStepTrigger = false;
	bool secondStepTrigger = false;

	private void Start()
	{
		firstStep = ScoreManager.maxScore / 3;
		firstStepTrigger = false;

		secondStep = (ScoreManager.maxScore * 2) / 3;
		secondStepTrigger = false;
	}

	private void Update()
	{
		if (!firstStepTrigger)
		{
			if (ScoreManager.score >= firstStep)
			{
				GainWallJump();
				firstStepTrigger = true;
			}
		}

		if (!secondStepTrigger)
		{
			if (ScoreManager.score >= secondStep)
			{
				PastequeRain();
				secondStepTrigger = true;
			}
		}
	}

	void GainWallJump()
	{
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<WallJump>().wallJumpIsUnlock = true;
		EventHandler.GainWallJump?.Invoke(); 
	}

	void PastequeRain()
	{
		EventHandler.StartPastequeRain?.Invoke(); 
	}
}
