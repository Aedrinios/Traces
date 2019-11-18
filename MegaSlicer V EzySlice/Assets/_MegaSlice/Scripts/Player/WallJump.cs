using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPS_Controller))]
[RequireComponent(typeof(CharacterController))]
public class WallJump : MonoBehaviour
{
	public LayerMask layerMask;
	public float radius = 1;
	public int maxWallJump = 3;

	int countJump = 0; 

    FPS_Controller fps;
    CharacterController characterController;   

    private void Start()
    {
        fps = GetComponent<FPS_Controller>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {        
        if (!fps.canJump)
        {
            ResetCanJump();
        }

		if (characterController.isGrounded && countJump != 0)
		{
			countJump = 0;
		}

    }

    void ResetCanJump()
    {
        if (!characterController.isGrounded && countJump < maxWallJump)
        {
			Vector3 center = transform.position + new Vector3(0, -0.25f, 0);

			Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);
 
            if (colliders.Length > 1)
            {
                fps.canJump = true;
				countJump++;

            }
        }
    }



}
