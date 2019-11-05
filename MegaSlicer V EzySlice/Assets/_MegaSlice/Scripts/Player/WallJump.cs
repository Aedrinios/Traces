using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPS_Controller))]
[RequireComponent(typeof(CharacterController))]
public class WallJump : MonoBehaviour
{
    public float radius = 1;


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

    }

    void ResetCanJump()
    {
        if (!characterController.isGrounded)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
 
            if (colliders.Length > 1)
            {
                fps.canJump = true;
            }
        }
    }

}
