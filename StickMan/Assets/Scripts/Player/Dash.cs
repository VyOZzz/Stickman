using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Dash : MonoBehaviour
    {
        private PlayerCtrl _playerCtrl;
        private bool isDashing = false;
        [SerializeField]private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [FormerlySerializedAs("dashCooldownTime")] [SerializeField] private float dashingCooldown = 5f;
        [FormerlySerializedAs("dashForce")] [SerializeField]private float dashingPower = 2000f;
        private float dashingTime = 0.2f;
        private bool canDash = true;
        [SerializeField] private TrailRenderer _trailRenderer;

        public bool IsDashing
        {
            get => isDashing;
        }
        public bool CanDash
        {
            get => canDash;
        }
        public void LoadCtrl(PlayerCtrl playerCtrl) => _playerCtrl = playerCtrl;
        private void Reset()
        {
            LoadCtrl(_playerCtrl);
        }
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            // xét detect của 2 layer khi start game
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        }
        public IEnumerator DashCoroutine()
        {
            isDashing = true;
            canDash = false;
            // xét gravity về 0 khi dash
            float originalGravity = _rigidbody2D.gravityScale;
            _rigidbody2D.gravityScale = 0;
            //không detect va chạm với 2 layer này
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            // dashhhhhhh
            _rigidbody2D.linearVelocity = new Vector2(dashingPower * transform.parent.localScale.x, 0f);
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            _trailRenderer.emitting = false;
            // bật lại detect va chạm với 2 layer này
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            // xét gravity về original khi dash
            _rigidbody2D.gravityScale = originalGravity;
            isDashing = false;
            // cooldown
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
