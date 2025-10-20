using System;
using UnityEngine;
using App.Code.Core.Input;
using App.Code.Character.Base;
using App.Code.Core.Systems.AnimationSystem;
using App.Code.Core.Systems.CombatSystem;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.JumpSystem;
using App.Code.Core.Systems.LockOnSystem;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using App.Code.Core.Systems.Movement;
using App.Code.Core.Systems.Movement.Interfaces;
using App.Code.Core.Systems.Movement.Structs;
using Unity.Cinemachine;

namespace App.Code.Character.Player
{
    public class PlayerController : SoulCharacter
    {
        [Header("Player Settings")] [SerializeField]
        private Transform cameraTransform;
        
        [Header("Cinemachine")]
        public CinemachineCamera normalCamera;   // ✅ CinemachineCamera (not VirtualCamera)
        public CinemachineCamera lockOnCamera;   // ✅ CinemachineCamera

        [Header("Movement Configuration")] public MovementConfig movementConfig;

        [Header("Jump Configuration")] public JumpConfig jumpConfig;

        [Header("Animation Configuration")] public AnimationConfig animationConfig;

        [Header("Combat Configuration")] public CombatConfig attackConfig;

        [Header("LockOn Configuration")] public LockOnConfig lockOnConfig;

        [Header("Movement Speed State")] private IMovementSpeedState _movementSpeedState;

        // Events
        public event Action<int> OnLevelUp;
        public event Action<int> OnExperienceGained;


        // Private Fields
        private IInputService _inputService;
        private AnimationSystem _animationSystem;
        private IMovementSystem _movementSystem;
        private IJumpable _jumpSystem;
        private ILockOnSystem _lockOnSystem;

        private float _currentSpeed;
        private bool _wasGrounded;
        private bool _jumpRequested;
        private float _blendValue;


        protected override void Awake()
        {
            _inputService = new InputPC();
            _jumpSystem = new JumpSystem(rb, transform, jumpConfig);
            _animationSystem = new AnimationSystem(animator, animationConfig);
            movementSystem = new MovementSystem(rb, transform, cameraTransform, movementConfig);
            _movementSpeedState = new MovementSpeedState(movementConfig, animationConfig);
            _lockOnSystem = new LockOnSystem(transform, lockOnConfig);
        }

        private void Start()
        {
            // Freeze rotation to prevent player from tipping over
            rb.freezeRotation = true;
        }

        private void Update()
        {
            // Handle jump input
            if (_inputService.JumpPressed)
            {
                Debug.Log("Jump key pressed!");
                _jumpSystem.Jump();
            }

            if (_inputService.IsLockedOn)
            {
                _lockOnSystem.ToggleLockOn();
            }

            if (_inputService.SwitchTargetLeft)
            {
                _lockOnSystem.SwitchTarget(Vector2.left); // Switch left
            }

            if (_inputService.SwitchTargetRight)
            {
                _lockOnSystem.SwitchTarget(Vector2.right); // Switch right
            }

            // Update lock-on state
            _lockOnSystem.UpdateLockOn();

            //Get character current speed for movement and animation blend tree
            MovementStateData state = _movementSpeedState.GetMovementState(_inputService);

            _currentSpeed = state.Speed;
            _blendValue = state.BlendValue;


            // Update jump system (coyote time, buffer, etc.)
            _animationSystem.UpdateJumpAnimation(_jumpSystem);
            _jumpSystem.UpdateJump();


            // Get raw input for animation (UPDATED)
            Vector3 moveInput = _inputService.InputDirection;
            bool isLockedOn = _lockOnSystem.IsLockedOn();

            // Normal movement animation (only when NOT locked on)
            if (!isLockedOn)
            {
                _animationSystem.UpdateMovementAnimation(_blendValue);
            }

            _animationSystem.UpdateLockOnAnimation(_lockOnSystem, moveInput);

            //_animationSystem.UpdateAttackAnimation(_attackSystem);
        }

        private void FixedUpdate()
        {
            if (rb == null) return;

            // Determine movement speed
            var inputVector = _inputService.InputDirection;
            bool canMove = !_animationSystem.IsInLandingAnimation();

            movementSystem.Move(new Vector3(inputVector.x, 0, inputVector.y), _currentSpeed);

            // ✅ CHECK IF LOCKED ON
            if (_lockOnSystem.IsLockedOn() && _lockOnSystem.GetCurrentTarget() != null)
            {
                // Rotate towards target (strafe mode)
                movementSystem.RotateTowardsTarget(_lockOnSystem.GetCurrentTarget());
            }
            else
            {
                // Normal rotation towards movement direction
                movementSystem.Rotate(new Vector3(inputVector.x, 0, inputVector.y));
            }

            if (canMove)
            {
            }
        }
        
        void LateUpdate()
        {
            UpdateLockOnCamera(); 
        }
        
        private void UpdateLockOnCamera()
        {
            if (_lockOnSystem.IsLockedOn() && _lockOnSystem.GetCurrentTarget() != null)
            {
                lockOnCamera.enabled = true;
                normalCamera.enabled = false;
        
                lockOnCamera.Follow = transform;
                lockOnCamera.LookAt = _lockOnSystem.GetCurrentTarget();
        
                // ✅ DEBUG
                Debug.Log($"[Camera] Follow: {lockOnCamera.Follow?.name}, LookAt: {lockOnCamera.LookAt?.name}");
            }
            else
            {
                normalCamera.enabled = true;
                lockOnCamera.enabled = false;
        
                lockOnCamera.Follow = null;
                lockOnCamera.LookAt = null;
            }
        }

        public void OnLandAnimationComplete()
        {
            // There is an event on Land animation and this function is called at the end of the Fall animation
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var pos = jumpConfig.groundCheckTransform.position;
            Gizmos.DrawLine(pos, pos + Vector3.down * jumpConfig.groundCheckDistance);
        }

        void OnDrawGizmosSelected()
        {
            if (lockOnConfig == null) return;

            // Draw lock-on radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, lockOnConfig.lockOnRadius);

            // Draw break distance
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lockOnConfig.breakDistance);

            //Draw current target
            if (_lockOnSystem != null && _lockOnSystem.IsLockedOn())
            {
                Transform target = _lockOnSystem.GetCurrentTarget();
                if (target != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, target.position);
                    Gizmos.DrawWireSphere(target.position, 0.5f);
                }
            }
        }
    }
}