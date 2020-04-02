using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSoundManager : MonoBehaviour
{
    public WallJump wallJumpScript;

    public GameObject jumpSd;
    public GameObject wallJumpSd;

    public void SpawnJumpPrefab()
    {
        if (wallJumpScript.canWallJump)
        {
            Instantiate(wallJumpSd, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(jumpSd, transform.position, transform.rotation);
        }

    }
}
