using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker
{
    private JumpController jumpController;

    private bool isGrounded = false;

    public void Init(JumpController jumpController)
    {
        this.jumpController = jumpController;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(jumpController.JumpCount > 0)
            {
                Debug.Log("바닥 점프리셋");
                jumpController.ResetJump();
            }
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("바닥에서 떨어짐");
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}