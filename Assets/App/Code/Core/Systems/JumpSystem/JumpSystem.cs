using System.Collections;
using App.Code.Character.Player;
using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.JumpSystem
{
    public class JumpSystem : MonoBehaviour, IJumpable
    {
        [Header("Jump Settings")] 
        public float jumpForce = 5f;
        public float jumpForwardForce = 6f;

        private Rigidbody rb;
        private PlayerController _playerController;
        private bool canMove;

        public JumpSystem(Rigidbody rb, PlayerController playerController)
        {
            this.rb = rb;
            _playerController = playerController;
        }


        public void Jump()
        {
            
            // Vertical
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Horizontal (committed - can't change much in air)
            if (_playerController.InputService.IsSprinting)
            {
                Vector3 forwardBoost = _playerController.transform.forward * jumpForwardForce;
                rb.AddForce(forwardBoost, ForceMode.Impulse);

                // Optional: Disable input briefly for committed jump
                StartCoroutine(DisableInputBriefly(0.3f));
            }
        }

        // Optional: Lock controls briefly for committed jump feel
        IEnumerator DisableInputBriefly(float duration)
        {
            canMove = false;
            yield return new WaitForSeconds(duration);
            canMove = true;
        }
    }
}