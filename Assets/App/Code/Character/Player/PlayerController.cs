using System;
using App.Code.Character.Base;
using App.Code.Character.Player.States;
using App.Code.Core.FSM;
using App.Code.Core.Systems.AnimationSystem;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.Movement;
using Assets.App.Code.Character.Player.States;
using Assets.App.Code.Core.Input;
using UnityEngine;

namespace App.Code.Character.Player
{
    public class PlayerController : SoulCharacter
    {
        [Header("Player Settings")] [SerializeField]
        private Transform cameraTransform;

        [SerializeField] private float interactionRange = 2f;

        [Header("Movement Settings")] [SerializeField]
        private float walkSpeed = 2f;

        [SerializeField] private float runSpeed = 4f;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float rotateSpeed = 4f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float airControlSpeed = 2f;

        // Events
        public event Action<int> OnLevelUp;
        public event Action<int> OnExperienceGained;

        // Public Properties
        public float InteractionRange => interactionRange;
        public Animator Anim { get; private set; }
        public IInputService InputService { get; private set; }
        public IMovementSystem MovementSystem { get; private set; }
        public IAnimationSystem AnimationSystem { get; private set; }


        // Public Fields
        public float checkDistance = 0.1f;
        public LayerMask groundMask;

        // Private Fields
        private IInputService _inputService;

        // Private States
        private PlayerIdleState _playerIdleState;
        private PlayerWalkState _playerWalkState;
        private PlayerRunState _playerRunState;
        private PlayerJumpState _playerJumpState;
        private PlayerSprintState _playerSprintState;
        private PlayerCrouchState _playerCrouchState;


        protected override void Awake()
        {
            _inputService = new InputPC();
            movementSystem = new MovementSystem(rb, cameraTransform);
            AnimationSystem = new AnimationSystem(animator, 0.1f);

            // Properties
            InputService = _inputService;
            MovementSystem = movementSystem;
            Anim = animator;

            // State Machine
            stateMachine = new StateMachine();

            // Initialize states
            _playerIdleState = new PlayerIdleState(this);
            _playerWalkState = new PlayerWalkState(this);
            _playerRunState = new PlayerRunState(this);
            _playerJumpState = new PlayerJumpState(this);
            _playerSprintState = new PlayerSprintState(this);
            _playerCrouchState = new PlayerCrouchState(this);
        }

        private void Start()
        {
            // Freeze rotation to prevent player from tipping over
            rb.freezeRotation = true;


            SetupStateTransitions();
            stateMachine.ChangeState(_playerIdleState);
        }

        private void Update()
        {
            
            stateMachine.UpdateState();
        }

        private void FixedUpdate()
        {

            stateMachine.UpdatePhysics();
        }

        private void SetupStateTransitions()
        {
            var ts = stateMachine.stateTransitionSystem;
            
            // From Idle
            ts.AddStateTransition(_playerIdleState,  () => InputService.HasMovementInput, _playerRunState,(int)StatePriority.Normal);
            ts.AddStateTransition(_playerIdleState, () => InputService.HasMovementInput && InputService.IsWalking, _playerWalkState,(int)StatePriority.Low);
            ts.AddStateTransition(_playerIdleState, () => InputService.HasMovementInput, _playerRunState, (int)StatePriority.Default);
            
            // From Walk
            ts.AddStateTransition(_playerWalkState, () => !InputService.HasMovementInput,_playerIdleState,(int )StatePriority.Default);
            ts.AddStateTransition(_playerWalkState, () => !InputService.IsWalking, _playerRunState,(int)StatePriority.Low); // Release walk key -> run

            // From Run
            ts.AddStateTransition(_playerRunState, () => !InputService.HasMovementInput, _playerIdleState,(int)StatePriority.Default);
            ts.AddStateTransition(_playerRunState, () => InputService.IsWalking, _playerWalkState,(int)StatePriority.Low);
            
        }

        public bool IsGrounded()
        {
            // Cast a short ray downward to detect ground
            return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundMask);
        }
    }
}