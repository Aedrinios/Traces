using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDirection : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 screenCenter;
    public Vector3 direction { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        direction = (mousePos - screenCenter).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(direction);
        }
    }
}
