using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlayerAttack : MonoBehaviour
{
    public GameObject prefabSlice;
    public Transform cutPlane;

    public float DelaySpamShot = 0.1f;
	[HideInInspector] public bool canShot = true; 	

    private void Start()
    {
		canShot = true; 
		Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShot)
        {
            LaunchProjectile();
			canShot = false;
			Invoke("ResetCanShot", DelaySpamShot); 
		}
    }

    public void LaunchProjectile()
    {
        Vector3 positionInstance = transform.position;
        positionInstance.y = transform.position.y + 0.8f;
        Instantiate(prefabSlice, positionInstance, cutPlane.rotation);
    }

	void ResetCanShot()
	{
		canShot = true;
	}
}
