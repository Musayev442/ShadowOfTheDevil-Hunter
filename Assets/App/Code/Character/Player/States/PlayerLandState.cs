using App.Code.Core.FSM;
using UnityEngine;

namespace App.Code.Character.Player.States
{
    public class PlayerLandState:IState
    {
        readonly PlayerController _playerController;

        public PlayerLandState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
            // Debug.Log("Enter LandState");
            // _playerController.AnimationSystem.SetGrounded(_playerController.IsGrounded);
            // _playerController.CanMove = false;
        }

        public void ExecuteUpdate()
        {
            //if(_playerController.Anim.)
        }

        public void ExecutePhysics()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}