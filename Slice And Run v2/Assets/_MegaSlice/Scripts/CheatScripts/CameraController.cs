using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 8;
    public float speedRotation = 100;
    Vector3 dirMove;
    Vector3 rotateVector;

    private void FixedUpdate()
    {
        NormalMove(); 
        RotateMove();
        ModifSpeed(); 

    }

    void NormalMove() 
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            float h = Input.GetAxis("HorizontalFr");
            float v = Input.GetAxis("VerticalFr");
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                dirMove = new Vector3(h, 0, v);
                dirMove = transform.TransformDirection(dirMove);
                dirMove.y = 0;
                transform.position += dirMove * speed * Time.deltaTime;
            }
            else
            {
                dirMove = new Vector3(h, v, 0);
                dirMove = transform.TransformDirection(dirMove);
                transform.position += dirMove * speed * Time.deltaTime;
            }
        }
    }

    void RotateMove()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float h = Input.GetAxis("HorizontalFr");
            float v = Input.GetAxis("VerticalFr");
            rotateVector = new Vector3(-v, h, 0);
            transform.eulerAngles += rotateVector * speedRotation * Time.deltaTime;
        }
    }

    void ModifSpeed()
    {
        float s = Input.GetAxis("Mouse ScrollWheel");
        speed += s; 
    }
}
