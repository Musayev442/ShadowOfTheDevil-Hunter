using App.Code.Character.Player;
using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.JumpSystem
{
    public class JumpSystem : IJumpable
    {
        private Rigidbody _rb;
        private Transform _transform;
        private JumpConfig _config;

        private bool _isGrounded;
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private bool _hasJumped;
        private bool _justJumped; // Track if jump just happened this frame

        public JumpSystem(Rigidbody rigidbody, Transform transform, JumpConfig config)
        {
            _rb = rigidbody;
            _transform = transform;
            _config = config;

            _isGrounded = false;
            _coyoteTimeCounter = 0f;
            _jumpBufferCounter = 0f;
            _hasJumped = false;
            _justJumped = false;
        }

        public void Jump()
        {
            _jumpBufferCounter = _config.jumpBufferTime;
        }

        public void UpdateJump()
        {
            // Reset just jumped flag at start of update
            _justJumped = false;

            // Ground check
            _isGrounded = CheckGround();

            // Update coyote time
            if (_isGrounded)
            {
                _coyoteTimeCounter = _config.coyoteTime;
                _hasJumped = false;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            // Update jump buffer
            if (_jumpBufferCounter > 0f)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            // Execute jump if conditions are met
            if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f && !_hasJumped)
            {
                PerformJump();
            }

            // Apply custom gravity for better jump feel
            if (_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_config.gravityScale - 1) * Time.deltaTime;
            }
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }

        public bool HasJustJumped()
        {
            return _justJumped;
        }

        private void PerformJump()
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            _rb.linearVelocity += Vector3.up * _config.jumpForce;

            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
            _hasJumped = true;
            _justJumped = true; // Set flag for animation system

            Debug.Log("[JumpSystem] Jump executed!");
        }

        private bool CheckGround()
        {
            Vector3 origin = _config.groundCheckTransform.position;
            bool hit =  Physics.Raycast(origin, Vector3.down, _config.groundCheckDistance, _config.groundLayer);
            return hit;
        }
    }
}