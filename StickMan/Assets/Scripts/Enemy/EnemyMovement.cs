using Manager;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform player;
        [SerializeField] private float moveSpeed = 10f;
        private bool isFacingRight =false;
        //[SerializeField] private float KBForce;
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemySwordAttack enemySwordAttack;
        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            _animator = GetComponent<Animator>();
            enemySwordAttack = GetComponentInChildren<EnemySwordAttack>();
        }

        void Update()
        {
            if(player != null && enemySwordAttack.CanMove )
            {
                MoveToPlayer();
                FlipDirection();
            }
        }
        void MoveToPlayer()
        {
            if (AnimationStrings.isWalk != null) 
                _animator.SetBool(AnimationStrings.isWalk, true); 
            Vector2 newPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position =  Vector2.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime);
        }
        private void FlipDirection()
        {
            if (player.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            
            }else if (player.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
        }
        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }

    
    }
}
