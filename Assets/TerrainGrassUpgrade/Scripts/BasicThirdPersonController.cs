using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThirdPersonController : MonoBehaviour {

    public float speed = 6.0f;
    public float runSpeed = 9.0f;
    public float rotateSpeed = 90f;

    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded  = false;
 
private void Update()
    {
        if (grounded)
        {
            // We are grounded, so recalculate movedirection directly from axes
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical")); //Determine the player's forward speed based upon the input.

            moveDirection = transform.TransformDirection(moveDirection); //make the direction relative to the player.

            if (Input.GetButton("Jump"))
                moveDirection *= runSpeed;
            else
                moveDirection *= speed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        CharacterController controller = GetComponent<CharacterController>();

        var flags = controller.Move(moveDirection * Time.deltaTime);
        transform.Rotate(0, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0);

        grounded = (CollisionFlags.CollidedBelow) != 0;
    }
}
