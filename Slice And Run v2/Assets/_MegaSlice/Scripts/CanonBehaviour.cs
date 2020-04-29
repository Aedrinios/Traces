using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public Transform muzzle;
    public Transform turret;

    private Transform target;
    private int layerMask = 1 << 15;


    private bool isReloading = true;
    [SerializeField] private float reloadTimer;
    [SerializeField] private bool followPlayer;
    public bool canShoot;
    public float offSetYTarget = 0.2f; 
    private float timer = 0f;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        layerMask = ~layerMask;
        turret.LookAt(target.transform.position + Vector3.up * offSetYTarget);
    }

    void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward + Vector3.up * -0.01f, Color.red);
        if (ChronoSystem.playing)
        {
            RaycastHit hit;
            Vector3 rayDirection = target.position - muzzle.position;
            if (Physics.Raycast(muzzle.position, rayDirection, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.tag == "Player")
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                }
            }
            if (followPlayer)
            {
                turret.LookAt(target.transform.position + Vector3.up * offSetYTarget);
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
                if (canShoot)
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(turret.forward));
        BulletBehaviour bulletBehaviour = bullet.transform.GetChild(0).GetComponent<BulletBehaviour>();
        bulletBehaviour.direction = muzzle.forward;
        isReloading = true;
    }
}
