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

    // le coyateTime est intégré 
    public float coyoteTime = 0.15f; 

    public float maxFallSpeed = 60;
    public float groundFriction = 20;
    public float raycastSize = 1;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool canJump = true;

    public UnityEvent Jump;

    //la variable velocity permet de bouger le personnage
    //la variable moveDir permet au joueur de contrôler l'avatar
    [HideInInspector] public Vector3 moveDir = Vector3.zero;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    CharacterController characterController;
    float cameraRotationX = 0;

    [HideInInspector] public bool isPushed = false; 
    [HideInInspector] public bool canMoveCamera = true;
    [HideInInspector] public bool canPlay = true;
    [HideInInspector] public Vector3 jumpDirection = new Vector3(0, 1, 0);

    public static Vector3 playerPos;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraRotationX = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMoveCamera = false;
        StopPlayer();
        Invoke("StartPlayer", 0.4f);
    }

    private void Update()
    {
        CheckOnGround();
        Gravity();        
        DoJump();
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
            moveDir += velocity;
        }
        else
        {
            moveDir = Vector3.zero;
            moveDir = transform.TransformDirection(moveDir);
            moveDir += velocity;
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
        if (!onGround)
        {      
            if (-velocity.y <= maxFallSpeed)
            {
                velocity.y -= gravity * Time.deltaTime;
            }
            VerticalFriction(0.2f);
        }
        else
        {
            if (!isPushed)
            {
                velocity.y = -0.1f;
                VerticalFriction(1);
            }
        }
    }

    public void DoJump()
    {
        if (onGround) canJump = true;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            BlockJump();
            Jump.Invoke();
            jumpDirection = transform.TransformDirection(jumpDirection);

            velocity += jumpDirection * jumpForce;          
            jumpDirection = new Vector3(0, 1, 0);
        }
    }

    void CheckOnGround()
    {
        if (characterController.isGrounded)
        {
            onGround = true; 
        }
        else if (Physics.Raycast(transform.position, -Vector3.up, raycastSize, gameObject.layer))
        {
            onGround = true;
        }
        else
        {
            if (onGround && canJump)
            {
                Invoke("BlockJump", coyoteTime); 
            }
            onGround = false;
        }
    }

    void BlockJump()
    {
         canJump = false;
    }

    public void VerticalFriction(float multiplier)
    {
        float gap = 0.1f; 

        if (velocity.x > gap)
        {
            velocity.x -= groundFriction * multiplier * Time.deltaTime; 
        }
        else if (velocity.x < -gap)
        {
            velocity.x += groundFriction * multiplier * Time.deltaTime;
        }

        if (velocity.z > gap)
        {
            velocity.z -= groundFriction * multiplier * Time.deltaTime;
        }
        else if (velocity.z < -gap)
        {
            velocity.z += groundFriction * multiplier * Time.deltaTime;
        }
    }

    public void PushPlayer(Vector3 pushDir)
    {
        velocity += pushDir;
        isPushed = true;
        Invoke("StopPush", 0.5f); 
    }

    void StopPush()
    {
        isPushed = false;
    }

    public void StopPlayer()
    {
        canPlay = false;
        canMoveCamera = false; 
    }

    public void StartPlayer()
    {
        isPushed = false;
        canPlay = true;
        canMoveCamera = true; 
    }
}
