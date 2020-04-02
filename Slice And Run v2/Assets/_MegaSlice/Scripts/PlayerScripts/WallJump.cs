using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPS_Controller))]
public class WallJump : MonoBehaviour
{
	public LayerMask layerMask;
	public float radius = 1;
    public float offSet_Y = -0.25f; 
	public int maxWallJump = 3;
    public float wallFriction = 20;
    public bool canWallJump = false; 

	int countJump = 0;
    bool wallNear;
    bool sliceObjectNear;

    FPS_Controller fps;

    private void Start()
    {
        fps = GetComponent<FPS_Controller>();
    }

    private void Update()
    {
        if (!fps.onGround)
        {
            DetectIfWallNear();

            // condition du wallJump
            if (wallNear && countJump <= maxWallJump)
            {
                fps.canJump = true;
                canWallJump = true; 
            }            
            else if (!wallNear)
            {
                fps.canJump = false;
                canWallJump = false;
            }
        }
        else
        {
            canWallJump = false;
        }

        //reset du compte de WallJump
        if (fps.onGround && countJump != 0)
		{
			countJump = 0;
		}		
    }

    private void FixedUpdate()
    {
        if (!fps.onGround && wallNear)
        {
            //wallFriction
            if (!fps.isPushed)
            {
                if (fps.velocity.y < 0)
                {
                    fps.velocity.y += wallFriction * Time.deltaTime;                    
                }
                if (!sliceObjectNear) fps.VerticalFriction(0.7f); 
            }
        }
    }

    void DetectIfWallNear()
    {
        Vector3 center = transform.position + new Vector3(0, offSet_Y, 0);
        Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);

        if (colliders.Length > 0) wallNear = true; 
        else wallNear = false;

        sliceObjectNear = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag == "Sliceable")
            {
                sliceObjectNear = true;
            }
        }
    }

    public void CountWallJump()
    {
        if (canWallJump)
        {
            //remet velocity à 0
            fps.velocity.y = 0;
            countJump++;
        }
    }
}
