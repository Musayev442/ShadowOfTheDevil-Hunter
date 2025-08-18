using App.Code.Movement.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.Movement
{
    public class BasicJump : IJumper
    {
        private Rigidbody rb;
        private float jumpBufferTime = 0.2f;
        private float jumpBufferCounter = 0f;

        public float jumpStartTime { get; set; }

        public BasicJump(Rigidbody rb)
        {
            this.rb = rb;
        }

        public void UpdateJumpBuffer(bool isJumping)
        {
            // Increment the jump buffer counter
            if (isJumping)
                jumpBufferCounter = jumpBufferTime;
            else
                jumpBufferCounter = Mathf.Max(0f, 
                    jumpBufferCounter - Time.deltaTime);
        }

        public void Jump(float force, bool isGrounded)
        {
            if (jumpBufferCounter > 0f && isGrounded)
            {
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);
                jumpBufferCounter = 0f;
            }
            jumpStartTime = Time.time;
        }
    }

}