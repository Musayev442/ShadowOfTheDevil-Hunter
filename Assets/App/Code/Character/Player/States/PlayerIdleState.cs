using App.Code.Character.Player;
using Assets.App.Code.Core.FSM;
using App.Code.Character.Player.States;
using UnityEngine;

namespace Assets.App.Code.Character.Player.States
{
    public class PlayerIdleState : IState
    {
        private readonly PlayerController _playerController;

        public PlayerIdleState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
             Debug.Log("Entering Idle State");
            _playerController.AnimationSystem.SetMovementSpeed(0);
        }

        public void ExecuteUpdate()
        {
            // Debug.Log("ExecuteUpdate Idle State");
            _playerController.AnimationSystem.SetMovementSpeed(0);
            //_playerController.movementSystem.Move(Vector3.zero, 0f, 0f, _playerController.movementSystem.deceleration);
        }

        public void ExecutePhysics()
        {
            // Debug.Log("ExecutePhysics Idle State");
            // Stop movement
            _playerController.MovementSystem.Stop();
        }

        public void Exit()
        {
             Debug.Log("Exiting Idle State");
            // Cleanup if needed
        }
    }
}