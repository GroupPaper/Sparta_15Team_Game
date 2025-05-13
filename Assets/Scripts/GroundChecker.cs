using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player _player;
    private JumpController _jumpController;
    private SlideController _slideController;
    private Transform _playerTransform;

    private bool isGrounded = false;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private GameObject runObject;
    [SerializeField] private GameObject jumpObject;
    [SerializeField] private GameObject slideObject;
    [SerializeField] private GameObject hitObject;

    public void Init(JumpController jumpController, SlideController slideController, 
    Transform playerTransform, Player player)
    {
        _jumpController = jumpController;
        _slideController = slideController;
        _playerTransform = playerTransform;
        _player = player;
    }

    void Update()
    {
        if (_playerTransform == null) return;

        // 어떤 오브젝트가 active 상태인지 가져오기
        GameObject activeObj;
        if (_slideController.IsSliding())
        {
            activeObj = slideObject;
        }
        else if (_jumpController.IsJumping() && !_player.IsInvincible())
        {
            activeObj = jumpObject;
        }
        else if (_player.IsInvincible())
        {
            activeObj = hitObject;
        }
        else
        {
            activeObj = runObject;
        }

        // 그 오브젝트에서 Collider2D 꺼내기
        Collider2D col = activeObj.GetComponent<Collider2D>();
        if (col == null) return; // 없으면 스킵

          // 콜라이더 높이의 절반
        float originY = col.bounds.min.y + 0.15f;        // 콜라이더 맨 밑(min.y) 바로 아래
        Vector2 boxOrigin = new Vector2(
            _playerTransform.position.x,
            originY
        );
        Vector2 boxSize = new Vector2(col.bounds.size.x, 0.05f);

        // BoxCast
        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // 5디버그
        Vector2 tl = new Vector2(col.bounds.min.x, originY);
        Vector2 tr = new Vector2(col.bounds.max.x, originY);
        Vector2 bl = tl + Vector2.down * groundCheckDistance;
        Vector2 br = tr + Vector2.down * groundCheckDistance;

        Debug.DrawLine(tl, tr, Color.yellow);
        Debug.DrawLine(bl, br, Color.yellow);
        Debug.DrawLine(tl, bl, Color.yellow);
        Debug.DrawLine(tr, br, Color.yellow);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_jumpController.JumpCount > 0)
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