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

        [Header("Movement")]
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 4f;
        [SerializeField] private float sprintSpeed = 6f;

        [Header("Combat/Actions")]
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float rollForce = 7f;

        public bool isLockedOn = false;

        private bool _isSprinting = false;
        private bool _isJumping = false;
        private bool _isWalking = false;
        private bool _isGrounded = false;
        private float targetSpeed = 0f;
        private float _horizontal = 0f;
        private float _vertical = 0f;

        public LayerMask groundLayer; // Layer for ground detection
        public Transform groundCheck; // Position to check if the character is grounded
        public float groundCheckRadius = 0.2f; // Radius for ground check



            Vector3 _velocity;
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
            _isWalking = playerInput.GetWalkInput();

            Vector3 inputDir = new Vector3(_horizontal, 0, _vertical).normalized;
            // Decide target speed
            
            if (inputDir.sqrMagnitude > 0.01f)
            {
                targetSpeed = _isSprinting ? sprintSpeed :
                              (_isWalking ? walkSpeed : runSpeed);
            }
            _velocity = Vector3.MoveTowards(
              new Vector3(_velocity.x, 0, _velocity.z), // ← Use current velocity
              inputDir * targetSpeed,
              10f * Time.deltaTime
           );

            float speed01 = Mathf.Clamp01(_velocity.magnitude / runSpeed);
            animator.SetFloat("Speed", _velocity.magnitude, 0.1f, Time.deltaTime);



            //animator.SetBool("isLockedOn", isLockedOn);

            //if (isLockedOn)
            //{
            //    characterAnimation.UpdateLockedOnMovementAnimation(_horizontal, _vertical);
            //}
            //else
            //{
            //    Vector3 move = new Vector3(_horizontal, 0, _vertical);
            //    characterAnimation.UpdateFreeMovementAnimation(move);
            //}
        }
        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(_horizontal, 0.0f, _vertical);
            Vector3 moveDir = GetCameraRelativeDirection(dir, cameraTransform);

            movement.Move(moveDir, targetSpeed);
            movement.RotateTowards(moveDir);

            _jumper.Jump(jumpForce, _isGrounded);
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
