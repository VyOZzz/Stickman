using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private PlayerCtrl _playerCtrl;
// sử dụng reset để chỉ việc reset là sẽ tự gán lại các component
    private void Reset()
    {
        LoadCtrl(out _playerCtrl);
    }

    private void LoadCtrl(out PlayerCtrl playerCtrl) => playerCtrl =  FindObjectOfType<PlayerCtrl>();
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void Update()
    {
        WalkHandle();
    }

    private void WalkHandle()
    {
        // nhận input từ người chơi với các phím như AD hay mũi tên
        float horInput = Input.GetAxis("Horizontal");
        // di chuyển
        rb.velocity = new Vector2(horInput * speed , rb.velocity.y);
        
    }
}
