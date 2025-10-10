using Assets.App.Code.Character.Player;
using Assets.App.Code.Core.FSM;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerSprintState : IState
    {
        private PlayerController _playerController;
        private Vector2 _inputVector;

        public PlayerSprintState(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public void Enter()
        {
        }

        public void ExecuteUpdate()
        {
            // Debug.Log("ExecutePhysics Sprint State");
            _playerController.AnimationSystem.SetMovementSpeed(1.5f);
        }

        public void ExecutePhysics()
        {
            _inputVector = _playerController.InputService.InputDirection;
            _playerController.MovementSystem.Move(new Vector3(_inputVector.x, 0, _inputVector.y), 18);
            _playerController.MovementSystem.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y), _playerController.transform);
        }

        public void Exit()
        {
            
        }
    }
}