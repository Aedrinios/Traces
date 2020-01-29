using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlayerAttack : MonoBehaviour
{
    public GameObject prefabSlice;
    public Transform cutPlane;

    public float DelaySpamShot = 0.1f;
	private bool canShot; 	

    private void Start()
    {
		canShot = true; 
    }

    public void Update()
    {
        if (Input.GetButtonDown("Projectile") && canShot)
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
