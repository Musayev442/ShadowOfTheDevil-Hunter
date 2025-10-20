
using App.Code.Core.FSM;
using App.Code.Character.Player;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerRunState : IState
    {
        private readonly PlayerController _playerController;
        private Vector2 _inputVector;

        public PlayerRunState(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public void Enter()
        {
            // Debug.Log("Entering Move State");
        }

        public void ExecuteUpdate()
        {
            //Debug.Log("ExecuteUpdate Move State");
            // Update animation with running input (will apply run multiplier)
            //_playerController.AnimationSystem.SetMovementSpeed(1f);
        }

        public void ExecutePhysics()
        {
            // Debug.Log("ExecutePhysics Move State");
            // _inputVector = _playerController.InputService.InputDirection;
            // _playerController.MovementSystem.Move(new Vector3(_inputVector.x, 0, _inputVector.y), 18);
            // _playerController.MovementSystem.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y), _playerController.transform);
        }

        public void Exit()
        {
            // Debug.Log("Exiting Move State");
        }
        
    }
}