/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject body; // Body ������Ʈ (�ִϸ��̼ǿ�)
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isisSliding = false; // �����̵� ����

    void Start()
    {
        animator = body.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isisSliding = false;
    }

    void Update()
    {
        // �׻� ���������� �̵�
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // ���� �Է� ó��
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

        // �����̵� �Է� ó��
        if (Input.GetKey(KeyCode.LeftShift)) // �����̵� Ű�� ������ ���� ��
        {
            isisSliding = true;
            animator.SetBool("isSliding", true); // �����̵� �ִϸ��̼� ���
        }
        else
        {
            isisSliding = false;
            animator.SetBool("isSliding", false); // �����̵� �ִϸ��̼� ����
        }

        // �ִϸ��̼� ó�� (�ʿ��)
        animator.SetInteger("jumpCount", jumpCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) // �ٴڰ� �浹
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}*/