using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float destroyTimer;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        destroyTimer -= Time.time;
        if(destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
