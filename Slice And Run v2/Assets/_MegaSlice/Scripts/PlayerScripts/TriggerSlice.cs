using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlice : MonoBehaviour
{
    public float loseLife = 0.04f;
    public ProjectileBehaviour projectile; 
    public GameObject hitParticle;
    public GameObject particleEndPrefab;
    public Transform cutPlane;

    public virtual void Slice(GameObject hit)
    {
        if (hit == null)
        {
            return;
        }        
        hit.GetComponent<SliceableObject>().Slice(cutPlane);
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            Instantiate(hitParticle, transform.position, transform.rotation);
            Vector3 forwardPorjectille = transform.forward; 
            other.gameObject.GetComponent<SliceableObject>().dirPush = forwardPorjectille; 
            Slice(other.gameObject);
            projectile.LoseLife(loseLife); 
        }

    }
}
