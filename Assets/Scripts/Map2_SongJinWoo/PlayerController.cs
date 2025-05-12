/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject body; // Body 오브젝트 (애니메이션용)
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isisSliding = false; // 슬라이드 여부

    void Start()
    {
        animator = body.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isisSliding = false;
    }

    void Update()
    {
        // 항상 오른쪽으로 이동
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount = 1;
                isGrounded = false;
            }
            else if (jumpCount == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount = 2;
            }
        }

        // 슬라이드 입력 처리
        if (Input.GetKey(KeyCode.LeftShift)) // 슬라이드 키를 누르고 있을 때
        {
            isisSliding = true;
            animator.SetBool("isSliding", true); // 슬라이드 애니메이션 재생
        }
        else
        {
            isisSliding = false;
            animator.SetBool("isSliding", false); // 슬라이드 애니메이션 멈춤
        }

        // 애니메이션 처리 (필요시)
        animator.SetInteger("jumpCount", jumpCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) // 바닥과 충돌
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}*/