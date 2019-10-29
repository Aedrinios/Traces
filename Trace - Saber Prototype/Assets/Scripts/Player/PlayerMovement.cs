using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 newDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (newDir.sqrMagnitude > 1)
        {
            newDir.Normalize();
        }

        // hauteur du sol ou de l'endroit du personnage
        var forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        var right = transform.right;
        right.y = 0;
        right.Normalize();

        var mouvement = forward * newDir.z + right * newDir.x;

        transform.Translate(mouvement * Time.deltaTime * speed, Space.World);
    }
}
