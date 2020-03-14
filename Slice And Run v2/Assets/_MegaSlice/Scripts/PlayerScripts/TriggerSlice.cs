using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlice : MonoBehaviour
{
    public float loveLife = 0.05f;
    public ProjectileBehaviour projectille; 
    public GameObject hitParticle;
    public Transform cutPlane;

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
            projectille.LoseLife(loveLife); 
        }
    }
}
