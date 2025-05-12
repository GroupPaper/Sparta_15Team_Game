using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController
{
    private float jumpPower;
    private int maxJumpCount;
    private int currentJumpCount;
    private bool isJumping = false;

    public int JumpCount => currentJumpCount;
    public bool IsJumping() => isJumping;

    public void Init(float jumpPower, int maxJumpCount)
    {
        this.jumpPower = jumpPower;
        this.maxJumpCount = maxJumpCount;
        currentJumpCount = 0;
        isJumping = false;
    }

    public bool TryJump()
    {
        if (currentJumpCount < maxJumpCount)
        {
            currentJumpCount++;
            isJumping = true;
            return true;
        }
        return false;
    }

    public void ResetJump()
    {
        currentJumpCount = 0;
        isJumping = false;
    }
}
