using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private PlayerCtrl _playerCtrl;

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
        float horInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horInput * speed , rb.velocity.y);
        
    }
}
