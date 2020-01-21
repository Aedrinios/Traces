using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOnSpawn : MonoBehaviour
{
    void Start()
    {
        transform.position = GameManager.Instance.playerPosition.position;
        transform.rotation = GameManager.Instance.playerPosition.rotation;
    }
}
