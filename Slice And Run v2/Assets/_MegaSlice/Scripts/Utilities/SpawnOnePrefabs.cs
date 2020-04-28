using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnePrefabs : MonoBehaviour
{
    public GameObject prefabSpawn;
    public string tagPrefab; 

    void Awake()
    {
        SpawnOnlyOnePrefabs(); 
    }

    void SpawnOnlyOnePrefabs()
    {
        GameObject oldPrefab = GameObject.FindGameObjectWithTag(tagPrefab); 
        if (oldPrefab == null)
        {
            Instantiate(prefabSpawn, transform.position, transform.rotation); 
        }
    }
    
}
