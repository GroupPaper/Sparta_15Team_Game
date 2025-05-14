using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Transform _playerTransform;

    private bool isGrounded = false;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private Collider2D playerCollider;
    // 박스 체크 위치 계산용 변수
    private float checkY;
    private float checkX;

    public void Init(Transform playerTransform, Player player)
    {
        _playerTransform = playerTransform;
        // 콜라이더가 설정되지 않은 경우 가져오기
        if (playerCollider == null)
        {
            playerCollider = player.GetComponent<Collider2D>();
        }
        // 바닥 체크 위치 계산용 값
        checkY = playerCollider.bounds.min.y + 0.05f;
        checkX = playerCollider.bounds.size.x * 0.9f;
    }

    void Update()
    {
        if (_playerTransform == null) return;
        // 콜라이더 크기를 기반으로 박스 크기
        checkY = playerCollider.bounds.min.y + 0.05f;
        checkX = playerCollider.bounds.size.x * 0.9f;
        // 박스 중심 위치 설정
        Vector2 boxOrigin = new Vector2(_playerTransform.position.x, checkY);
        // 박스 크기 설정
        Vector2 boxSize = new Vector2(checkX, groundCheckDistance);

        isGrounded = Physics2D.OverlapBox(boxOrigin, boxSize, 0f, groundLayer);

        // 디버그
        Vector2 tl = boxOrigin + Vector2.left * boxSize.x / 2;
        Vector2 tr = boxOrigin + Vector2.right * boxSize.x / 2;
        Vector2 bl = tl + Vector2.down * boxSize.y;
        Vector2 br = tr + Vector2.down * boxSize.y;
        Debug.DrawLine(tl, tr, Color.yellow);   //상
        Debug.DrawLine(bl, br, Color.yellow);   //하
        Debug.DrawLine(tl, bl, Color.yellow);   //좌
        Debug.DrawLine(tr, br, Color.yellow);   //우
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}