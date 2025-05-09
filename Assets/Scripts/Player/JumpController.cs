using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class JumpController
// {
//     private float jumpForce;
//     private int maxJumpCount;
//     private int jumpCount;
//     private bool isJumping;

//     public void Init(float jumpForce, int maxJumpCount)
//     {
//         this.jumpForce = jumpForce;
//         this.maxJumpCount = maxJumpCount;
//         jumpCount = 0;
//         isJumping = false;
//     }

//     public void TryJump()
//     {
//         if (jumpCount < maxJumpCount)
//         {
//             isJumping = true;
//         }
//     }

//     public Vector2 ApplyJump(Vector2 currentVelocity)
//     {
//         if (isJumping)
//         {
//             currentVelocity.y = jumpForce;
//             jumpCount++;
//             isJumping = false;
//         }
//         return currentVelocity;
//     }

//     public void ResetJump()
//     {
//         jumpCount = 0;
//     }
// }

public class JumpController
{
    private float jumpForce;
    private int maxJumpCount;
    private int jumpCount;
    private bool isJumping;

    private float jumpStartTime; // 점프 시작 시간
    private float jumpDuration = 0.5f; // 점프가 끝나는 데 걸리는 시간

    // 속도가 오를수록 점프 듀레이션 감소

    public void Init(float jumpForce, int maxJumpCount)
    {
        this.jumpForce = jumpForce;
        this.maxJumpCount = maxJumpCount;
        jumpCount = 0;
        isJumping = false;
        jumpStartTime = 0f;
    }

    public void TryJump()
    {
        if (jumpCount < maxJumpCount)
        {
            isJumping = true;
            jumpStartTime = Time.time; // 점프 시작 시간 기록
        }
    }

    public Vector2 ApplyJump(Vector2 currentVelocity)
    {
        if (isJumping)
        {
            float t = (Time.time - jumpStartTime) / jumpDuration; // 0에서 1까지의 시간 비율
            // float sinValue = Mathf.Sin(Mathf.PI * t); // 사인 함수를 이용한 점프 높이 변화

            // 점프 높이 계산
            currentVelocity.y = jumpForce; // * sinValue 

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

