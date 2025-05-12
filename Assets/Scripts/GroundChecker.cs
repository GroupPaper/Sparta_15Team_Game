using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private JumpController _jumpController;
    private SlideController _slideController;

    private bool isGrounded = false;

    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer; 
    private Transform playerTransform;

    public void Init(JumpController jumpController, SlideController slideController, Transform playerTransform)
    {
        this._jumpController = jumpController;
        this._slideController = slideController;
        this.playerTransform = playerTransform;  // playerTransform을 초기화
    }

    public void Update()
    {
        // 바닥을 Raycast로 체크 (캐릭터 아래로 ray 발사)
        // isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (playerTransform == null) return;

        float offsetY = _slideController.IsSliding() ? 0.4f : 0.6f;
        // Vector3 groundCheckPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - offsetY, playerTransform.position.z);

        // // 레이캐스트로 바닥을 체크
        // isGrounded = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, groundLayer);

        // // 디버깅용
        // Debug.DrawRay(groundCheckPosition, Vector2.down * groundCheckDistance, Color.red);

        Vector2 boxSize = new Vector2(0.5f, 0.2f); // 박스 크기 (너비는 캐릭터 폭, 높이는 얇게)
        Vector2 boxOrigin = new Vector2(playerTransform.position.x, playerTransform.position.y - offsetY);

        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        Debug.DrawRay(boxOrigin, Vector2.down * groundCheckDistance, hit.collider != null ? Color.green : Color.red);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(_jumpController.JumpCount > 0)
            {
                Debug.Log("바닥 점프리셋");
                _jumpController.ResetJump();
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