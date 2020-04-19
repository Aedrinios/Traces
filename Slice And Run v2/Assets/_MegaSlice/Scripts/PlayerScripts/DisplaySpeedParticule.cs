using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySpeedParticule : MonoBehaviour
{    
    public float speedTrigger = 10f;
    FPS_Controller fps;
    Renderer psRenderer;

    Color originalColor;
    Color invisibleColor;
    
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); 
        if (player != null) fps = player.GetComponent<FPS_Controller>();

        psRenderer = GetComponent<Renderer>();
        originalColor = psRenderer.material.color;
        invisibleColor = originalColor;
        invisibleColor.a = 0; 
    }

    void Update()
    {
        if (fps != null)
        {
            transform.position = fps.gameObject.transform.position;
            if (fps.moveDir.magnitude >= speedTrigger)
            {
                psRenderer.material.color = originalColor;
            }
            else
            {
                psRenderer.material.color = invisibleColor;
            }
            ChangeRotation();
        }
    }

    void ChangeRotation()
    {
        //Vector3 moveRect = new Vector3(fps.moveDir.x, 0, fps.moveDir.z);
        Vector3 moveRect = fps.moveDir; 
        moveRect = moveRect.normalized;

        float angleY = Vector3.Angle(moveRect, Vector3.forward);
        if (moveRect.x < 0) angleY = -angleY;

        float angleX = Vector3.Angle(moveRect, Vector3.up);
        angleX += -90; 

        Vector3 rotationParticule = new Vector3(angleX, angleY, 0);
        transform.rotation = Quaternion.Euler(rotationParticule);
        
    }
}
