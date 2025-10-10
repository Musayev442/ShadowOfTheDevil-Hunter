using Assets.App.Code.Character.Player;
using Assets.App.Code.Core.FSM;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerJumpState : IState
    {
        private readonly PlayerController _playerController;
        private bool _hasAppliedJumpForce;
        private Vector2 _inputVector;

        public PlayerJumpState(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public void Enter()
        {
            //Debug.Log("Entering Jump State");
            _playerController.AnimationSystem.SetGrounded(!_playerController.IsGrounded());
            _playerController.AnimationSystem.SetTrigger();
            _hasAppliedJumpForce = false;
        }

        public void ExecuteUpdate()
        {
            //Debug.Log("ExecuteUpdate Jump State: " + _playerController.IsGrounded());
        }

        public void ExecutePhysics()
        {
            //Debug.Log("ExecutePhysics Jump State");
            if (_hasAppliedJumpForce)return;
            _playerController.Jumpable.Jump();
            _hasAppliedJumpForce = true;
        }

        public void Exit()
        {
          //Debug.Log("Exiting Jump State: "+_playerController.IsGrounded());
        }
    }
}