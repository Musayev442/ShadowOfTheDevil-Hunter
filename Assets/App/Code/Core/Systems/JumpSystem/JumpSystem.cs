using App.Code.Character.Player;
using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.JumpSystem
{
    public class JumpSystem : IJumpable
    {
        [Header("Jump Settings")] 
        public float jumpHeight = 2f;
        public float jumpTime = 0.5f;
        public float jumpCooldown = 0.1f;

        private float jumpForce = 25f;
        private float jumpTimer;
        private float lastJumpTime;
        private bool isJumping;

        private Rigidbody _rb;
        private PlayerController _playerController;

        public JumpSystem(Rigidbody rb, PlayerController playerController)
        {
            _rb = rb;
            _playerController = playerController;
        }

        public void Jump()
        {
            _rb.AddForce(Vector3.up * (jumpForce * Time.fixedDeltaTime * 10f), ForceMode.Impulse);
        }
    }
}