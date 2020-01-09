using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    public float rayCastDistance = 0.1f;
    private CharacterController cc;

    private bool onPlatform = false;
    private MovingPlatform currentPlatform;
  
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var ray = new Ray(transform.position - transform.up * (cc.height * 0.5f), -transform.up);
        Debug.DrawRay(ray.origin, ray.direction * rayCastDistance, new Color(1f, 0.96f, 0.28f));
        if (Physics.Raycast(ray,  out RaycastHit hitInfo, cc.skinWidth + rayCastDistance, LayerMask.GetMask("Platforms")))
        {
            if(!onPlatform)
            {
                EnterPlatform(hitInfo.collider.GetComponent<MovingPlatform>());
            }
        }
        else if(onPlatform)
        {
            ExitPlatform();
        }
    }

    private void EnterPlatform(MovingPlatform movingPlatform)
    {
        if (movingPlatform == null) return;

        print("Enter " + movingPlatform.name);
        onPlatform = true;
        movingPlatform.Enter(cc);

        currentPlatform = movingPlatform;
    }

    private void ExitPlatform()
    {
        onPlatform = false;
        
        if (currentPlatform == null) return;
        print("Exit " + currentPlatform.name);

        currentPlatform.Exit();
    }
}
