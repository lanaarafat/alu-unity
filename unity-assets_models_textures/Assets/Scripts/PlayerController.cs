using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public float jumpForce = 5.0f; // Jump force when the player jumps.
    public float fallThreshold = -10.0f; // Y value below which the player will reset

    private Rigidbody rb; // Reference to player's Rigidbody.
    private bool isGrounded = true; // Check if the player is on the ground.
    private Vector3 startPosition; // Player's starting position

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.

        // Freeze rotation on X and Z axes to prevent the capsule from tipping over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground before allowing a jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            // isGrounded = false; // Set grounded to false after jumping.

            if (transform.position.y < fallThreshold)
            {
                ResetPlayerPosition();
            }
        }
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Get horizontal and vertical inputs for movement.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Move the player in the direction based on input.
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        /* Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation); */
    }

    // Check when the player is grounded to allow jumping again.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ensure this tag is set on ground objects.
        {
            isGrounded = true;
        }
    }

    private void ResetPlayerPosition()
    {

        // Temporarily disable gravity
        rb.isKinematic = true;

        // Rest the player's position to the starting postion
        transform.position = startPosition;

        // clear velocity and angular velocity to stop movement
        rb.velocity = Vector3.zero; // stop movement
        rb.angularVelocity = Vector3.zero; // stop rotation

        // re-enable physics
        rb.isKinematic = false;
       
    }
}
