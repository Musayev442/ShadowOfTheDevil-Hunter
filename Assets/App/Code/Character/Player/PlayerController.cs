using UnityEngine;
using UnityEngine.InputSystem;
using App.Code.Movement.Interfaces;
using App.Code.Movement;

namespace SotD.Characters.Player
{
    /// <summary>Drives the Player (input, state updates, animation hooks).</summary>
    //[RequireComponent(typeof(Player))]
    public class PlayerController : Character
    {
        [Header("Player Settings")]
        [SerializeField] private Transform cameraTransform;

        public bool isLockedOn = false;
        public float jumpForce = 5f;

        private bool _isSprinting = false;
        private bool _isJumping = false;
        private float _horizontal = 0f;
        private float _vertical = 0f;

        public LayerMask groundLayer; // Layer for ground detection
        public Transform groundCheck; // Position to check if the character is grounded
        public float groundCheckRadius = 0.2f; // Radius for ground check

        private bool isGrounded;


        IJumper _jumper;

        private void Start()
        {
            _jumper = new BasicJump(character_rb);
        }

        private void Update()
        {
            _horizontal = playerInput.GetHorizontalInput();
            _vertical = playerInput.GetVerticalInput();
            _isSprinting = playerInput.GetSprintInput();
            _isJumping = playerInput.GetJumpInput();

            // Check if the character is grounded
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


            _jumper.UpdateJumpBuffer(_isJumping);
            
             bool isFalling = Time.time > _jumper.jumpStartTime + 0.1f && character_rb.linearVelocity.y < -0.1f;
            if(_isJumping)
                characterAnimation.UpdateJumpAnimation(character_rb, _isJumping,isFalling, isGrounded);


            //animator.SetBool("isLockedOn", isLockedOn);

            if (isLockedOn)
            {
                characterAnimation.UpdateLockedOnMovementAnimation(_horizontal, _vertical);
            }
            else
            {
                Vector3 move = new Vector3(_horizontal, 0, _vertical);
                characterAnimation.UpdateFreeMovementAnimation(move);
            }
        }
        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(_horizontal, 0.0f, _vertical);
            Vector3 moveDir = GetCameraRelativeDirection(dir, cameraTransform);

            movement.Move(moveDir, speed);
            movement.RotateTowards(moveDir);

            _jumper.Jump(jumpForce, isGrounded);
        }

        public Vector3 GetCameraRelativeDirection(Vector3 inputDir, Transform cameraTransform)
        {
            // Camera forward/right projected onto XZ plane
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Convert input into world-space direction
            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

            return moveDir.normalized;
        }
    }
}
