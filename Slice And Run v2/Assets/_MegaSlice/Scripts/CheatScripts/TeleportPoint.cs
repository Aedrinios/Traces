using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public KeyCode keyTeleport;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(keyTeleport))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
}
