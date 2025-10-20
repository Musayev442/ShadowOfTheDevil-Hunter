using App.Code.Core.FSM;
using App.Code.Character.Player;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerWalkState : IState
    {
        private readonly PlayerController _playerController;
        private Vector3 _inputVector;

        public PlayerWalkState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
            // Debug.Log("Enter Walk State");
        }

        public void ExecuteUpdate()
        {
            // Debug.Log("ExecuteUpdate Walk State");
            //_playerController.AnimationSystem.SetMovementSpeed(0.5f);
        }

        public void ExecutePhysics()
        {
            // Debug.Log("ExecutePhysics Walk State");
            // _inputVector = _playerController.InputService.InputDirection;
            // _playerController.MovementSystem.Move(new Vector3(_inputVector.x, 0, _inputVector.y), 12);
            // _playerController.MovementSystem.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y), _playerController.transform);
        }

        public void Exit()
        {
            // Debug.Log("Exit Walk State");
        }
    }
}