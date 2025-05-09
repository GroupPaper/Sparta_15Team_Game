using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker
{
    private JumpController jumpController;

    public void Init(JumpController jumpController)
    {
        this.jumpController = jumpController;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpController.ResetJump();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        // 필요 시 처리
    }
}

