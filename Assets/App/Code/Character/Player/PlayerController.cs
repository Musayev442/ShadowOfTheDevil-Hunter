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

        [Header("Movement")]
        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 4f;
        [SerializeField] private float _sprintSpeed = 6f;

        [Header("Combat / Actions")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _rollForce = 7f;

        [Header("Ground Detection")]
        [SerializeField] private LayerMask _groundLayer;     // Layer for ground detection
        [SerializeField] private Transform _groundCheck;     // Position to check if the character is grounded
        [SerializeField] private float _groundCheckRadius = 0.2f;

        // State Machine
        [Header("State Machine")]
        [HideInInspector] public StateMachine stateMachine;

        // States
        [HideInInspector] public PlayerIdle idleState;
        [HideInInspector] public PlayerWalk walkingState;
        [HideInInspector] public PlayerRun runningState;
        [HideInInspector] public PlayerJump jumpingState;



        // === Runtime State ===
        // Input values (cached once per frame)
        [HideInInspector] public float horizontal;
        [HideInInspector] public float vertical;
        [HideInInspector] public bool isJumpPressed;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public bool isWalking;
        [HideInInspector] public Vector2 movementInput;
        [HideInInspector] public float movementMagnitude;

        private bool _isLockedOn = false;
        private bool _isWalking = false;
        private bool _isGrounded = false;

        private float _targetSpeed = 0f;
        private float _currentSpeed;

        // === Systems / Interfaces ===
        private IPlayerInput playerInput;
        private ISprintable _sprint;
        private IStamina _stamina;
        private IJumper _jumper;



        private void Start()
        {
            playerInput = new Input_PC();
            _jumper = new BasicJump(characterRigidbody);
            sprintAnimation = new CharacterAnimation(animator);
            _stamina = new StaminaSystem(100f);
            _sprint = new SprintController(_stamina, characterRigidbody, _sprintSpeed, 10f);
            stateMachine = new StateMachine();
            stateMachine.ChangeState(new PlayerIdle(this));
        }

        private void Update()
        {
            HandleInput();

            stateMachine.UpdateState();


            if (isSprinting && _sprint.CanSprint)
            {
                _sprint.Sprint(movementInput);
                _targetSpeed = _sprintSpeed;
                sprintAnimation.UpdateSprintAnimation(isSprinting);
            }
            else
            {
                if (movementMagnitude > 0.01f)
                {
                    _targetSpeed = isWalking ? _walkSpeed : _runSpeed;
                }
                _stamina.Regenerate(5f * Time.deltaTime);
                freeMovementAnimation.UpdateFreeMovementAnimation(movementInput, isWalking);
            }


        }
        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(horizontal, 0.0f, vertical);
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

        private void HandleInput()
        {
            horizontal = playerInput.GetHorizontalInput();
            vertical = playerInput.GetVerticalInput();
            movementInput = new Vector2(horizontal, vertical);
            movementMagnitude = movementInput.magnitude;

            isWalking = playerInput.GetWalkInput();
            isSprinting = playerInput.GetSprintInput();
            isJumpPressed = playerInput.GetJumpInput();
        }
    }
}
