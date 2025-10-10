using System;
using App.Code.Character.Base;
using App.Code.Character.Player.States;
using App.Code.Core.FSM;
using App.Code.Core.Systems.AnimationSystem;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.JumpSystem;
using App.Code.Core.Systems.Movement;
using Assets.App.Code.Character.Player.States;
using Assets.App.Code.Core.FSM;
using Assets.App.Code.Core.Input;
using UnityEngine;

namespace App.Code.Character.Player
{
    public class PlayerController : SoulCharacter
    {
        [Header("Player Settings")] 
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float interactionRange = 2f;

        [Header("Movement Settings")] 
        [SerializeField] private float walkSpeed = 2f;

        [SerializeField] private float runSpeed = 4f;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float rotateSpeed = 4f;
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float airControlSpeed = 2f;

        // Events
        public event Action<int> OnLevelUp;
        public event Action<int> OnExperienceGained;
        
        
        [Header("Public Properties")]
        public float InteractionRange => interactionRange;
        public bool CanMove { get; set; }
        public Animator Anim { get; private set; }
        public IInputService InputService { get; private set; }
        public IMovementSystem MovementSystem { get; private set; }
        public IAnimationSystem AnimationSystem { get; private set; }
        public Rigidbody Rb { get; private set; }
        
        [Header("Jump Properties")]
        public IJumpable Jumpable {get; private set;}
        public Vector3 Velocity => rb.linearVelocity;
        public bool IsRising => rb.linearVelocity.y > 0.1f;
        public bool IsFalling => rb.linearVelocity.y < -0.1f;
        public float TimeInAir { get; set; }
        

        
        // Public Fields
        public float checkDistance = 0.2f;
        public LayerMask groundMask;
        public Transform groundCheck;
    


        // Private Fields
        private IInputService _inputService;
        

        // Private States
        private PlayerIdleState _playerIdleState;
        private PlayerWalkState _playerWalkState;
        private PlayerRunState _playerRunState;
        private PlayerSprintState _playerSprintState;
        private PlayerJumpState _playerJumpState;
        private PlayerFallState _playerFallState;
        private PlayerLandState _playerLandState;
        private PlayerCrouchState _playerCrouchState;


        protected override void Awake()
        {
            _inputService = new InputPC();
            movementSystem = new MovementSystem(rb, cameraTransform);
            AnimationSystem = new AnimationSystem(animator, 0.1f);

            // Properties
            InputService = _inputService;
            MovementSystem = movementSystem;
            Jumpable = new JumpSystem(rb, this);
            Anim = animator;
            Rb = rb;
            // State Machine
            stateMachine = new StateMachine();

            // Initialize states
            _playerIdleState = new PlayerIdleState(this);
            _playerWalkState = new PlayerWalkState(this);
            _playerRunState = new PlayerRunState(this);
            _playerJumpState = new PlayerJumpState(this);
            _playerSprintState = new PlayerSprintState(this);
            _playerCrouchState = new PlayerCrouchState(this);
            _playerFallState = new PlayerFallState(this);
            _playerLandState = new PlayerLandState(this);
        }

        private void Start()
        {
            // Freeze rotation to prevent player from tipping over
            rb.freezeRotation = true;
            CanMove = true;

            SetupStateTransitions();
            stateMachine.ChangeState(_playerIdleState);
        }

        private void Update()
        {
            
            stateMachine.UpdateState();
        }

        private void FixedUpdate()
        {
            Debug.Log(IsGrounded());
            stateMachine.UpdatePhysics();

        }

        private void SetupStateTransitions()
        {
            var ts = stateMachine.stateTransitionSystem;
            
            // From Idle
            ts.AddStateTransition(_playerIdleState,  () => InputService.HasMovementInput, _playerRunState,(int)StatePriority.Normal);
            ts.AddStateTransition(_playerIdleState, () => InputService.HasMovementInput && InputService.IsWalking, _playerWalkState,(int)StatePriority.Low);
            ts.AddStateTransition(_playerIdleState, () => InputService.HasMovementInput, _playerRunState, (int)StatePriority.Default);
            ts.AddStateTransition(_playerIdleState, () => InputService.HasMovementInput && InputService.IsSprinting && CanSprint(), _playerSprintState, (int)StatePriority.Normal);
            ts.AddStateTransition(_playerIdleState, () => InputService.JumpPressed && IsGrounded(),_playerJumpState, (int)StatePriority.High);
            ts.AddStateTransition(_playerIdleState, () => ShouldBeInFallState(), _playerFallState, (int)StatePriority.High);
            
            // From Walk
            ts.AddStateTransition(_playerWalkState, () => !InputService.HasMovementInput,_playerIdleState,(int )StatePriority.Default);
            ts.AddStateTransition(_playerWalkState, () => !InputService.IsWalking, _playerRunState,(int)StatePriority.Low); // Release walk key -> run
            ts.AddStateTransition(_playerWalkState, () => InputService.HasMovementInput && InputService.IsSprinting && CanSprint(), _playerSprintState, (int)StatePriority.Normal);
            ts.AddStateTransition(_playerWalkState, () => InputService.JumpPressed && IsGrounded(),_playerJumpState, (int)StatePriority.High);
            ts.AddStateTransition(_playerWalkState, () => ShouldBeInFallState(), _playerFallState, (int)StatePriority.High);
            
            // From Run
            ts.AddStateTransition(_playerRunState, () => !InputService.HasMovementInput, _playerIdleState,(int)StatePriority.Default);
            ts.AddStateTransition(_playerRunState, () => InputService.IsWalking, _playerWalkState,(int)StatePriority.Low);
            ts.AddStateTransition(_playerRunState, () => InputService.HasMovementInput && InputService.IsSprinting && CanSprint(), _playerSprintState, (int)StatePriority.Normal);
            ts.AddStateTransition(_playerRunState, () => InputService.JumpPressed && IsGrounded(),_playerJumpState, (int)StatePriority.High);
            ts.AddStateTransition(_playerRunState, () => ShouldBeInFallState(), _playerFallState, (int)StatePriority.High);
            
            // From Sprint
            ts.AddStateTransition(_playerSprintState, () => !CanSprint(), _playerRunState, (int)StatePriority.High);
            ts.AddStateTransition(_playerSprintState, () => !InputService.IsSprinting, _playerRunState, (int)StatePriority.Normal);
            ts.AddStateTransition(_playerSprintState, () => !InputService.HasMovementInput, _playerIdleState, (int)StatePriority.Default);
            ts.AddStateTransition(_playerSprintState, () => InputService.JumpPressed && IsGrounded(),_playerJumpState, (int)StatePriority.High);
            ts.AddStateTransition(_playerSprintState, () => ShouldBeInFallState(), _playerFallState, (int)StatePriority.High);
            
            // From Jump
            ts.AddStateTransition(_playerJumpState, () => IsFalling, _playerFallState, (int)StatePriority.High);
            //ts.AddStateTransition(_playerJumpState, () => IsGrounded(), _playerLandState, (int)StatePriority.Critical);
            
            // From Fall
            ts.AddStateTransition(_playerFallState, () => IsGrounded(), _playerLandState, (int)StatePriority.Critical);
            
            // From Land
            ts.AddStateTransition(_playerLandState, () => CanMove, GetNextMovementState(), (int)StatePriority.Normal);
        }
        
        public IState GetNextMovementState()
        {
            if (!InputService.HasMovementInput) return _playerIdleState;
            if (InputService.IsSprinting && CanSprint()) return _playerSprintState;
            if (InputService.IsWalking) return _playerWalkState;
            return _playerRunState;
        }

        private bool CanSprint()
        {
            return true;
        }

        public bool IsGrounded()
        {
            // Cast a short ray downward to detect ground
            return Physics.Raycast(groundCheck.position, Vector3.down, checkDistance, groundMask);
        }
        
        public bool ShouldBeInFallState()
        {
            return !IsGrounded() && !IsRising && stateMachine.CurrentState != _playerJumpState;
        }

        public void OnLandAnimationComplete()
        {
            // There is an event on Land animation and this function is called at the end of the Fall animation
            CanMove = true;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.2f);
        }

    }
}