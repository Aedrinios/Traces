using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTest : MonoBehaviour
{
    private FPS_Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FPS_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.canMoveCamera = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            controller.canMoveCamera = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            RotateArrow.canRotate = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            RotateArrow.canRotate = true;
        }
    }
}
