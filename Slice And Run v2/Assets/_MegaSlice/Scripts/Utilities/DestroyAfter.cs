using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float timeAfterDestroy = 1f;

    private void Start()
    {
        Invoke("DestroyMe", timeAfterDestroy); 
    }

    void DestroyMe()
    {
        Destroy(gameObject); 
    }
}
