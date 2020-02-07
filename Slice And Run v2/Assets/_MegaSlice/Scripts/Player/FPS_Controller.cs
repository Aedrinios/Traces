using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    public Transform cameraHolder; 
    public float speed = 20;
    public float jumpForce = 20; 
    public float gravity = 20;
    public float sensivityX = 200;
    public float sensivityY = 200;

    [HideInInspector] public bool canJump = true;
	[HideInInspector] public bool canMoveCamera = true;
    [HideInInspector] public bool canPlay = true;

	public static Vector3 playerPos;
    

	CharacterController characterController;
    Vector3 moveDir = Vector3.zero;
    Vector3 velocityVertical = Vector3.zero;
    Vector3 jumpDirection; 
    float cameraRotationX = 0;

    Transform cameraPlayer; 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraRotationX = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMoveCamera = true;
        cameraPlayer = cameraHolder.GetComponentInChildren<Camera>().gameObject.transform; 
	}

	// attention la dernère fois que j'ai mis un fixedUpdate au lieu du Update, ça ne marchait plus
	private void Update()
    {
        Gravity();
		Jump();
		if (canMoveCamera) RotateWithMouse();
        DefineMoveDirection();
        characterController.Move(moveDir * Time.deltaTime);
		playerPos = transform.position;
    }



    void DefineMoveDirection()
    {
        if (canPlay)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 inputs = new Vector3(h, 0, v);
            if (inputs.magnitude >= 1) inputs = inputs.normalized;
            moveDir = inputs * speed;
            moveDir = transform.TransformDirection(moveDir);
            moveDir += velocityVertical; 
        }
        else
        {
            moveDir = Vector3.zero;
            moveDir = transform.TransformDirection(moveDir);
            moveDir += velocityVertical; 
        }
    }

    void RotateWithMouse()
    {
        float rotX = Input.GetAxis("Mouse X") * 10;
        float rotY = Input.GetAxis("Mouse Y") * 10;

        transform.Rotate(Vector3.up, rotX * sensivityX * Time.deltaTime);

        cameraRotationX += -rotY * sensivityY * Time.deltaTime;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -89, 89); 
        cameraHolder.transform.localEulerAngles = new Vector3(cameraRotationX, 0, 0);
    }

    void Gravity()
    {
        if (!characterController.isGrounded)
        {
            velocityVertical.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocityVertical = Vector3.zero; 
        }
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            canJump = true;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            jumpDirection = new Vector3(0, 1, 0); 
            jumpDirection = cameraPlayer.TransformDirection(jumpDirection); 
            //jumpDirection = 
            velocityVertical = jumpDirection * jumpForce;  
            canJump = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/InGame/Actions/PlayerCharacter/Saut", transform.position);
        }
    }

    public void StopPlayer()
    {
        canPlay = false;
        canMoveCamera = false; 
    }

    public void StartPlayer()
    {
        canPlay = true;
        canMoveCamera = true; 
    }
}
