using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeLose = 0.02F;
    [SerializeField] private float speed;
    public Transform cutPlane;
    public GameObject hitParticle;

    private void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Slice(GameObject hit)
    {
        if (hit == null)
        {
            return;
        }
        hit.GetComponent<SliceableObject>().Slice(cutPlane);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            Instantiate(hitParticle, transform.position, transform.rotation);
            Slice(other.gameObject);
            lifeTime -= lifeLose;
        }
        else
        {
            lifeTime -= lifeLose * 2.5f;
        }
    }
}

