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
    private CharacterController cc;
    private int layerMask = 1 << 15;

    private bool isReloading = true;
    [SerializeField] private float reloadTimer;
    [SerializeField] private bool followPlayer;
    public bool canShoot;

     [Header("Target Control")]

    public float offSetYTarget = 0.2f;
    public float targetVelocityAdjustement;
    private float timer = 0f;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cc = target.gameObject.GetComponent<CharacterController>();
        layerMask = ~layerMask;
        turret.LookAt(target.position + Vector3.up * offSetYTarget);
    }

    void Update()
    {
        Debug.DrawLine(target.position, turret.position, Color.red);
        if (ChronoSystem.playing)
        {
            RaycastHit hit;
            Vector3 rayDirection = target.position - turret.position;
            if (Physics.Raycast(turret.position, rayDirection, out hit, Mathf.Infinity, layerMask))
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
                Vector3 trueTarget = new Vector3(target.position.x + cc.velocity.x / targetVelocityAdjustement, target.position.y + cc.velocity.y / targetVelocityAdjustement, target.position.z + cc.velocity.z / targetVelocityAdjustement);
                Debug.DrawLine(turret.position, trueTarget, Color.red);

                turret.LookAt(trueTarget + Vector3.up * offSetYTarget);
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
