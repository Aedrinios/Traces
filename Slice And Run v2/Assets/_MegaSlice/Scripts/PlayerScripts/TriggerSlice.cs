using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlice : MonoBehaviour
{
    public float loveLife = 0.05f;
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

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            Debug.Log("transform rotation : " + transform.rotation);
            Instantiate(hitParticle, transform.position, transform.rotation);
            Slice(other.gameObject);
            projectile.LoseLife(loveLife); 
        }

    }
}
