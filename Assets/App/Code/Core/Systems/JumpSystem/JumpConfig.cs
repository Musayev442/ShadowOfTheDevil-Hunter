using UnityEngine;

namespace App.Code.Core.Systems.JumpSystem
{
    [System.Serializable]
    public class JumpConfig
    {
        [Header("Jump Parameters")]
        public float jumpForce = 8f;
        public float gravityScale = 2f;
        public float airControlMultiplier = 0.5f;
    
        [Header("Jump Timing")]
        public float coyoteTime = 0.15f;        // Grace period after leaving ground
        public float jumpBufferTime = 0.2f;     // Early jump input buffer
    
        [Header("Ground Detection")]
        public Transform groundCheckTransform;
        public float groundCheckDistance = 0.2f;
        public LayerMask groundLayer = 1;       // Default layer
    }
}