using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Transform muzzle;
    private Transform turret;
    private Transform target;
    private bool isReloading;
    [SerializeField] private float reloadTimer;
    [SerializeField] private bool followPlayer;
    private float timer = 0f;

    private void Start()
    {
        turret = transform.Find("Turret");
        muzzle = turret.Find("Muzzle");

        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(followPlayer)
            turret.LookAt(target);

        if (isReloading)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTimer)
            {
                timer = 0;
                isReloading = false;
            }
        }
        else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(turret.forward));
        isReloading = true;
    }
}
