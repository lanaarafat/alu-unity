using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float horizontalInput;
    public float verticalInput;
    public float jumpForce = 7f;
    public bool isGrounded;
    private Rigidbody rb;
    public Vector3 startPosition;
    public float minHeight = -10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        isFalling();
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(verticalInput, 0, -horizontalInput);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); 
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void isFalling()
    {
        if (transform.position.y < minHeight)
        {
            transform.position = startPosition + new Vector3(0, 10f, 0);
            rb.velocity = Vector3.zero;
        }
    }
}
