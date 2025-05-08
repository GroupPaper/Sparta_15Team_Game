using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody = null;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float maxSpeed = 10f;

    private int jumpCount = 0;

    private float totalAcceleration = 0f;

    private bool isJumping = false;
    private bool isGrounded = false;

    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            isJumping = true;
        }
    }

    public void FixedUpdate()
    {
        totalAcceleration += acceleration * Time.fixedDeltaTime;
        totalAcceleration = Mathf.Min(totalAcceleration, maxSpeed - forwardSpeed);
        
        float currentSpeed = forwardSpeed + totalAcceleration;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = currentSpeed;

        if (isJumping)
        {
            velocity.y = jumpForce;
            isJumping = false;
            jumpCount++;
        }

        _rigidbody.velocity = velocity;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
