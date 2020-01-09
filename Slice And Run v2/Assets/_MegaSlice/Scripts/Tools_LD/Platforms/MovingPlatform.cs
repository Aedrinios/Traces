using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
    public bool makePlayerFollowRotation = false;

    private CharacterController playerController;
    private bool playerTransported = false;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private FPS_Controller fpController;

 
    public void Enter(CharacterController characterController)
    {
        playerTransported = true;
        playerController = characterController;
        fpController = playerController.GetComponent<FPS_Controller>();
    }

    public void Exit()
    {
        playerTransported = false;
        playerController = null;
    }

    private void FixedUpdate()
    {
        if (playerTransported)
        {
            Vector3 velocity = transform.position - previousPosition;
            playerController.Move(velocity);

            if(makePlayerFollowRotation)
            {
                MakePlayerFollowRotation();
            }
        }

        previousPosition = transform.position;

        if (makePlayerFollowRotation)
            previousRotation = transform.rotation;
    }

    private void MakePlayerFollowRotation()
    {
        Vector3 localPlayerPos = playerController.transform.position - transform.position;
        Quaternion ft = transform.rotation * Quaternion.Inverse(previousRotation);
        Vector3 targetPos = ft * localPlayerPos;
        Vector3 targetVel = targetPos - localPlayerPos;
        
        Transform playerTsfm = playerController.transform;
        Vector3 rForward = ft * playerTsfm.forward;

        fpController.enabled = false;

        playerTsfm.forward = rForward;

        fpController.enabled = true;

        playerController.Move(targetVel);

        ////Debug
        //Vector3 pos = transform.position;// playerController.transform.position;
        //Debug.DrawLine(pos, pos + localPlayerPos, Color.green);
        //Debug.DrawLine(pos, pos + targetPos, Color.cyan);
        //Debug.DrawLine(playerController.transform.position, playerController.transform.position + targetVel, Color.magenta);
    }
}
