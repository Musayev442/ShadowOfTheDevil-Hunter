using App.Code.Movement;
using App.Code.Movement.Interfaces;
using Assets.App.Code.Animation.Interfaces;
using Assets.App.Code.Character.Player.States;
using Assets.App.Code.Character.System;
using Assets.App.Code.Character.System.Interfaces;
using Assets.App.Code.Movement;
using Assets.App.Code.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

namespace SotD.Characters.Player
{
    /// <summary>Drives the Player (input, state updates, animation hooks).</summary>
    //[RequireComponent(typeof(Player))]
    public class PlayerController : Character
    {
        // === Inspector Settings ===
        [Header("Player Settings")]
        [SerializeField] private Transform _cameraTransform;

        [Header("Movement Settings")]
        public float walkSpeed = 2f;
        public float runSpeed = 4f;
        public float sprintSpeed = 6f;
        public float jumpForce = 10f;
        public float airControlSpeed = 2f;

        [Header("Combat / Actions")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _rollForce = 7f;

        [Header("Ground Detection")]
        public LayerMask groundLayer = 1;
        public float groundCheckDistance = 0.1f;

        // State Machine
        [Header("State Machine")]
        [HideInInspector] public StateMachine stateMachine;

        // States
        [HideInInspector] public PlayerIdleState idleState;
        [HideInInspector] public PlayerWalkingState walkingState;
        [HideInInspector] public PlayerRunningState runningState;
        [HideInInspector] public PlayerJumpingState jumpingState;
        [HideInInspector] public PlayerSprintingState sprintingState;



        // === Runtime State ===
        // Input values (cached once per frame)
        [HideInInspector] public float horizontal;
        [HideInInspector] public float vertical;
        [HideInInspector] public bool jumpPressed;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public bool isWalking;
        [HideInInspector] public Vector3 movementDirection;
        [HideInInspector] public Vector2 movementInput;
        [HideInInspector] public float movementMagnitude;


        // === Systems / Interfaces ===
        private IPlayerInput playerInput;
        private ISprintable _sprint;
        private IStamina _stamina;
        private IJumper _jumper;



        private void Start()
        {
            playerInput = new Input_PC();
            _jumper = new BasicJump(rb);
            sprintAnimation = new CharacterAnimation(animator);
            _stamina = new StaminaSystem(100f);
            _sprint = new SprintController(_stamina, rb, sprintSpeed, 10f);


            // Initialize state machine
            stateMachine = new StateMachine();

            // Create states
            idleState = new PlayerIdleState(this);
            walkingState = new PlayerWalkingState(this);
            runningState = new PlayerRunningState(this);
            sprintingState = new PlayerSprintingState(this);
            jumpingState = new PlayerJumpingState(this);

            // Start with idle state
            stateMachine.ChangeState(idleState);
        }

        private void Update()
        {
            HandleInput();


            // Update the current state (handles state transitions and logic)
            stateMachine.UpdateState();


            // Calculate speed multiplier based on movement type
            float speedMultiplier = 1f; // Default running

            if (isWalking)
                speedMultiplier = 0.5f;      // Walking is slower
            else if (isSprinting)
                speedMultiplier = 1.5f;      // Sprinting is faster

            // For Blend Tree - use float parameters
            animator.SetFloat("Speed", movementMagnitude * speedMultiplier);
        }

        private void FixedUpdate()
        {
            // Update physics-based movement in current state
            stateMachine.UpdatePhysics();

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

        public bool IsGrounded()
        {
            // Simple ground check using raycast
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        }

        private void HandleInput()
        {
            horizontal = playerInput.GetHorizontalInput();
            vertical = playerInput.GetVerticalInput();
            movementInput = new Vector2(horizontal, vertical);
            movementDirection = GetCameraRelativeDirection(new Vector3(horizontal, 0, vertical), _cameraTransform);
            movementMagnitude = movementInput.magnitude;

            isWalking = playerInput.GetWalkInput();
            isSprinting = playerInput.GetSprintInput();
            jumpPressed = playerInput.GetJumpInput();
        }
    }
}
