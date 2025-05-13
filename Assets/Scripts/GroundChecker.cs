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

    private float checkY;
    private float checkX;

    public void Init(Transform playerTransform, Player player)
    {
        _playerTransform = playerTransform;

        if (playerCollider == null)
        {
            playerCollider = player.GetComponent<Collider2D>();
        }

        checkY = playerCollider.bounds.min.y + 0.05f;
        checkX = playerCollider.bounds.size.x * 0.9f;
    }

    void Update()
    {
        if (_playerTransform == null) return;

        checkY = playerCollider.bounds.min.y + 0.05f;
        checkX = playerCollider.bounds.size.x * 0.9f;

        Vector2 boxOrigin = new Vector2(_playerTransform.position.x, checkY);
        Vector2 boxSize = new Vector2(checkX, groundCheckDistance);

        isGrounded = Physics2D.OverlapBox(boxOrigin, boxSize, 0f, groundLayer);

        Vector2 tl = boxOrigin + Vector2.left * boxSize.x / 2;
        Vector2 tr = boxOrigin + Vector2.right * boxSize.x / 2;
        Vector2 bl = tl + Vector2.down * boxSize.y;
        Vector2 br = tr + Vector2.down * boxSize.y;
        Debug.DrawLine(tl, tr, Color.yellow);   
        Debug.DrawLine(bl, br, Color.yellow);
        Debug.DrawLine(tl, bl, Color.yellow);
        Debug.DrawLine(tr, br, Color.yellow);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}