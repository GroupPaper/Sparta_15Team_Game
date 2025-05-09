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

//=====================================================================
// public class JumpController
// {
//     private float jumpForce;
//     private int maxJumpCount;
//     private int jumpCount;
//     private bool isJumping;

//     private float jumpStartTime; // 점프 시작 시간
//     private float jumpDuration = 0.5f; // 점프가 끝나는 데 걸리는 시간

//     // 속도가 오를수록 점프 듀레이션 감소

//     public void Init(float jumpForce, int maxJumpCount)
//     {
//         this.jumpForce = jumpForce;
//         this.maxJumpCount = maxJumpCount;
//         jumpCount = 0;
//         isJumping = false;
//         jumpStartTime = 0f;
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
//             float t = (Time.time - jumpStartTime) / jumpDuration; // 0에서 1까지의 시간 비율
//             // float sinValue = Mathf.Sin(Mathf.PI * t); // 사인 함수를 이용한 점프 높이 변화

//             // 점프 높이 계산
//             currentVelocity.y = jumpForce; // * sinValue 

//             jumpCount++;
//             isJumping = false;
//         }
//         return currentVelocity;
//     }

//     public void ResetJump()
//     {
//         jumpCount = 0;
//     }

//     public int JumpCount
//     {
//         get { return jumpCount; }
//     }
// }

//===================================================================

// public class JumpController
// {
//     private float jumpForce;
//     private int maxJumpCount;
//     private int currentJumpCount;
//     private float jumpDisplacement;
//     private float gravity = -20f;

//     public int JumpCount => currentJumpCount;

//     private bool isJumping = false;

//     public void Init(float jumpForce, int maxJumpCount)
//     {
//         this.jumpForce = jumpForce;
//         this.maxJumpCount = maxJumpCount;
//         currentJumpCount = 0;
//         jumpDisplacement = 0f;
//     }

//     public bool TryJump()
//     {
//         if (currentJumpCount < maxJumpCount)
//         {
//             isJumping = true;
//             jumpDisplacement = jumpForce;
//             currentJumpCount++;
//             return true;
//         }
//         return false;
//     }

//     // public void ApplyGravity()
//     // {
//     //     if (isJumping)
//     //     {
//     //         jumpDisplacement += gravity * Time.deltaTime;
//     //     }
//     // }

//     public void ApplyGravity(bool isGrounded)
//     {
//         if (!isGrounded)
//         {
//             jumpDisplacement += gravity * Time.deltaTime;  // 중력 추가
//         }
//         else if (jumpDisplacement < 0f) // 바닥에 닿았을 때만 리셋
//         {
//             Debug.Log("확인");
//             jumpDisplacement = 0f; // 바닥에 닿았을 때만 Y축 이동을 초기화
//         }
//     }

//     public float GetJumpDisplacement()
//     {
//         return jumpDisplacement; //  * Time.deltaTime
//     }

//     public void ResetJump()
//     {
//         isJumping = false;
//         currentJumpCount = 0;
//         jumpDisplacement = 0f;
//     }

//     public bool IsJumping()
//     {
//         return isJumping;
//     }
// }

public class JumpController
{
    private float jumpPower;
    private int maxJumpCount;
    private int currentJumpCount;
    private float jumpDisplacement;
    private bool isJumping = false;

    public int JumpCount => currentJumpCount;
    public bool IsJumping() => isJumping;

    public void Init(float jumpPower, int maxJumpCount)
    {
        this.jumpPower = jumpPower;
        this.maxJumpCount = maxJumpCount;
        currentJumpCount = 0;
        jumpDisplacement = 0f;
        isJumping = false;
    }

    public bool TryJump()
    {
        if (currentJumpCount < maxJumpCount)
        {
            jumpDisplacement = jumpPower;
            currentJumpCount++;
            isJumping = true;
            return true;
        }
        return false;
    }

    public float GetJumpDisplacement()
    {
        float displacement = jumpDisplacement;
        jumpDisplacement = 0f;
        return displacement;
    }

    public void ResetJump()
    {
        currentJumpCount = 0;
        jumpDisplacement = 0f;
        isJumping = false;
    }
}
