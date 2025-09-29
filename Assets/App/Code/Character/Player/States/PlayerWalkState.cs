using Assets.App.Code.Character.Player;
using Assets.App.Code.Core.FSM;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerWalkState : IState
    {
        private readonly PlayerController _playerController;
        private Vector2 _inputVector;

        public PlayerWalkState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
            Debug.Log("Enter Walk State");
        }

        public void ExecuteUpdate()
        {
            Debug.Log("ExecuteUpdate Walk State");
            _playerController.AnimationSystem.UpdateMovementAnimation(_playerController.InputService.MovementInput);
        }

        public void ExecutePhysics()
        {
            Debug.Log("ExecutePhysics Walk State");
            _inputVector = _playerController.InputService.MovementInput;
            _playerController.MovementSystem.Move(new Vector3(_inputVector.x, 0, _inputVector.y), 12);
            _playerController.MovementSystem.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y), _playerController.transform);
        }

        public void Exit()
        {
            Debug.Log("Exit Walk State");
        }
    }
}