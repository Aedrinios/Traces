﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public Transform muzzle;
    public Transform turret;

    private Transform target;

    private bool isReloading = true;
    [SerializeField] private float reloadTimer;
    [SerializeField] private bool followPlayer;
    private float timer = 0f;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        turret.LookAt(target.transform);
    }

    void Update()
    {
        if (ChronoSystem.playing)
        {
            if (followPlayer)
            {
                turret.LookAt(target.transform);
            }

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
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(turret.forward));
        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.direction = muzzle.forward;
        isReloading = true;
    }
}
