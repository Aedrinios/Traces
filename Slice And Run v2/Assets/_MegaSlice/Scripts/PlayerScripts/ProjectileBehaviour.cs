using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void LoseLife(float lose)
    {
        lifeTime -= lose; 
    }
}

