using System;
using UnityEngine;

namespace App.Test.Scripts
{
    public class TestController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Rigidbody rb;
        public float moveSpeed = 5f;
        public float jumpForce = 7f;
        public float groundCheckDistance = 0.1f;
        public LayerMask groundLayer;
    
        private bool isGrounded;
        
        void FixedUpdate()
        {
            // Basic movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
    
            Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        }
        
        

       

        void Update()
        {
            CheckGrounded();
        
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        void CheckGrounded()
        {
            // Raycast downward to check if grounded
            RaycastHit hit;
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 
                groundCheckDistance, groundLayer);
        
            // Optional: Visualize the ray in Scene view
            Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, 
                isGrounded ? Color.green : Color.red);
        }

        void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}