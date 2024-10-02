using System;
using Player;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    private PlayerCtrl _playerCtrl;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask wallLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCtrl = GetComponent<PlayerCtrl>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !_playerCtrl.GroundChecker.IsGrounded )
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.transform.position.x,
                Mathf.Min(_rigidbody2D.linearVelocity.y, -1f));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
    }

    private bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, wallLayer);
    }
}
