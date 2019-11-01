using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    public Camera cameraPlayer; 
    public float speed = 20;
    public float jumpForce = 20; 
    public float gravity = 20;
    public float sensivityX = 200;
    public float sensivityY = 200;

    CharacterController characterController;
    Vector3 moveDir = Vector3.zero;
    float velocityVertical = 0;
    float cameraRotationX = 0;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraRotationX = 0;
        Cursor.visible = false; 
    }

    private void Update()
    {
        Gravity();
        Jump();
        RotateWithMouse(); 
        DefineMoveDirection();
        characterController.Move(moveDir * Time.deltaTime); 
    }

    void DefineMoveDirection()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputs = new Vector3(h, 0, v);
        if (inputs.magnitude >= 1) inputs = inputs.normalized;
        moveDir = inputs * speed;
        moveDir = transform.TransformDirection(moveDir); 

        moveDir.y = velocityVertical; 
    }

    void RotateWithMouse()
    {
        float rotX = Input.GetAxis("Mouse X") * 10;
        float rotY = Input.GetAxis("Mouse Y") * 10;

        transform.Rotate(Vector3.up, rotX * sensivityX * Time.deltaTime);

        cameraRotationX += -rotY * sensivityY * Time.deltaTime;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -89, 89); 
        cameraPlayer.transform.localEulerAngles = new Vector3(cameraRotationX, 0, 0);
    }

    void Gravity()
    {
        if (!characterController.isGrounded)
        {
            velocityVertical -= gravity * Time.deltaTime;
        }
        else
        {
            velocityVertical = -gravity * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocityVertical = jumpForce;
        }
    }
}
