using UnityEngine;
using UnityEngine.InputSystem;

namespace SotD.Characters.Player
{
    /// <summary>Drives the Player (input, state updates, animation hooks).</summary>
    //[RequireComponent(typeof(Player))]
    public class PlayerController : Character
    {
        public bool isLockedOn = false;

        private float _horizontal;
        private float _vertical;

        

        private void Start()
        {
            //player.Initialize();
        }

        private void Update()
        {
            _horizontal = playerInput.GetHorizontalInput();
            _vertical = playerInput.GetVerticalInput();

            animator.SetBool("isLockedOn", isLockedOn);

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
            movement.Move(dir, speed);
        }

    }
}
