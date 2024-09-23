using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private PlayerCtrl _playerCtrl;
    private Animator _animator;
    private bool isFacingRight = true;
    private float _horInput;
    [SerializeField] private bool isWalk;
    
// sử dụng reset để chỉ việc reset là sẽ tự gán lại các component
    private void Reset()
    {
        LoadCtrl(out _playerCtrl);
    }
    
    private void LoadCtrl(out PlayerCtrl playerCtrl) => playerCtrl =  FindObjectOfType<PlayerCtrl>();
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        WalkHandle();
        
    }

    private void WalkHandle()
    {
        // nhận input từ người chơi với các phím như AD hay mũi tên
        _horInput = Input.GetAxis("Horizontal");
        
        WalkState();
        // set animation walk
        _animator.SetBool("isWalk", isWalk);
        FlipDirection();
        // di chuyển
        rb.velocity = new Vector2(_horInput * speed , rb.velocity.y);
    }
    private void FlipDirection()
    {
        // lớn hơn 0.1 để nó không tự động quay sang bên phải khi nó về 0
        if (_horInput > 0.1)
        {
            if (!isFacingRight)
            {
                transform.parent.localScale *= new Vector2(-1, 1);
                isFacingRight = true;
            }
        }
        else if(_horInput < 0)
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                transform.parent.localScale *= new Vector2(-1, 1);
            }
        }
    }

    private void WalkState()
    {
        if (Mathf.Abs(_horInput) > 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
    }
    
}
