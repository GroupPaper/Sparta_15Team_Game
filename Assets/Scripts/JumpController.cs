using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController
{
    private float jumpForce;
    private int maxJumpCount;
    private int jumpCount;
    private bool isJumping;

    public void Init(float jumpForce, int maxJumpCount)
    {
        this.jumpForce = jumpForce;
        this.maxJumpCount = maxJumpCount;
        jumpCount = 0;
        isJumping = false;
    }

    public void TryJump()
    {
        if (jumpCount < maxJumpCount)
        {
            isJumping = true;
        }
    }

    public Vector2 ApplyJump(Vector2 currentVelocity)
    {
        if (isJumping)
        {
            currentVelocity.y = jumpForce;
            jumpCount++;
            isJumping = false;
        }
        return currentVelocity;
    }

    public void ResetJump()
    {
        jumpCount = 0;
    }
}

