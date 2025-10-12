using Assets.App.Code.Core.FSM;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerFallState : IState
    {
        readonly PlayerController _playerController;

        public PlayerFallState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
            Debug.Log("Falling State");
        }

        public void ExecuteUpdate()
        {
            //Debug.Log(_playerController.Velocity.y);
           _playerController.AnimationSystem.SetVerticalVelocity(_playerController.Velocity.y);
           _playerController.AnimationSystem.SetGrounded(_playerController.IsGrounded);
        }

        public void ExecutePhysics()
        {
        }

        public void Exit()
        {
            
        }
    }
}