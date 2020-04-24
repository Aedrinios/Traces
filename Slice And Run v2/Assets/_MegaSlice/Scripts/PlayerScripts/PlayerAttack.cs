using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlayerAttack : MonoBehaviour
{
    public GameObject prefabSlice;
    public Transform canon; 
    public Transform cutPlane;
    public float DelaySpamShot = 0.1f;

    public float forcePushCut = 80;
    public static float forcePushCutStc;

    public float ratioPush = 0.4f;
    public static float ratioPushStc;

    [HideInInspector] public bool canShot;
    private bool startGame;

    private void Awake()
    {
        forcePushCutStc = forcePushCut;
        ratioPushStc = ratioPush; 
        canShot = false;
        startGame = true; 
    }

    public void Update()
    {
        if (Input.GetButtonDown("Projectile") && canShot)
        {
            LaunchProjectile();
			canShot = false;
			Invoke("ResetCanShot", DelaySpamShot); 
		}
        DelockShooter();
    }

    public void LaunchProjectile()
    {
        Vector3 positionInstance = canon.position;

        Instantiate(prefabSlice, positionInstance, cutPlane.rotation);
    }

	void ResetCanShot()
	{
		canShot = true;
	}

    void DelockShooter()
    {
        if (Input.anyKeyDown && startGame)
        {
            startGame = false;
            canShot = true; 
        }
    }
}
