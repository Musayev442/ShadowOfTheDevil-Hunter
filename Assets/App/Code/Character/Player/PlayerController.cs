using App.Code.Movement;
using App.Code.Movement.Interfaces;
using Assets.App.Code.Animation.Interfaces;
using Assets.App.Code.Character.System;
using Assets.App.Code.Character.System.Interfaces;
using Assets.App.Code.Movement;
using Assets.App.Code.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SotD.Characters.Player
{
    /// <summary>Drives the Player (input, state updates, animation hooks).</summary>
    //[RequireComponent(typeof(Player))]
    public class PlayerController : Character
    {
        // === Inspector Settings ===
        [Header("Player Settings")]
        [SerializeField] private Transform _cameraTransform;

        [Header("Movement")]
        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 4f;
        [SerializeField] private float _sprintSpeed = 6f;
        [SerializeField] private float _acceleration = 10f;

        [Header("Combat / Actions")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _rollForce = 7f;

        [Header("Ground Detection")]
        [SerializeField] private LayerMask _groundLayer;     // Layer for ground detection
        [SerializeField] private Transform _groundCheck;     // Position to check if the character is grounded
        [SerializeField] private float _groundCheckRadius = 0.2f;

        // === Runtime State ===
        private bool _isLockedOn = false;
        private bool _isSprinting = false;
        private bool _isJumping = false;
        private bool _isWalking = false;
        private bool _isGrounded = false;

        private float _horizontal = 0f;
        private float _vertical = 0f;
        private float _targetSpeed = 0f;
        private float _currentSpeed;

        // === Systems / Interfaces ===
        private StateMachine _stateMachine;
        private IJumper _jumper;
        private IStamina _stamina;
        private ISprintable _sprint;



        private void Start()
        {
            _jumper = new BasicJump(characterRigidbody);
            sprintAnimation = new CharacterAnimation(animator);
            _stamina = new StaminaSystem(100f);
            _sprint = new SprintController(_stamina, characterRigidbody, _sprintSpeed, 10f);
            _stateMachine = new StateMachine();
            _stateMachine.ChangeState(new PlayerIdle(this));
        }

        private void Update()
        {
            _stateMachine.UpdateState();

            _horizontal = playerInput.GetHorizontalInput();
            _vertical = playerInput.GetVerticalInput();
            _isSprinting = playerInput.GetSprintInput();
            _isJumping = playerInput.GetJumpInput();
            _isWalking = playerInput.GetWalkInput();

            Vector3 inputDir = new Vector3(_horizontal, 0, _vertical).normalized;



            if (_isSprinting && _sprint.CanSprint)
            {
                _sprint.Sprint(inputDir);
                _targetSpeed = _sprintSpeed;
                sprintAnimation.UpdateSprintAnimation(_isSprinting);
            }
            else
            {
                if (inputDir.sqrMagnitude > 0.01f)
                {
                    _targetSpeed = _isWalking ? _walkSpeed : _runSpeed;
                }
                _stamina.Regenerate(5f * Time.deltaTime);
                freeMovementAnimation.UpdateFreeMovementAnimation(inputDir, _isWalking);
            }


        }
        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(_horizontal, 0.0f, _vertical);
            Vector3 moveDir = GetCameraRelativeDirection(dir, _cameraTransform);

            movement.RotateTowards(moveDir);
            movement.Move(moveDir, _targetSpeed);
            _jumper.Jump(_jumpForce, _isGrounded);
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
