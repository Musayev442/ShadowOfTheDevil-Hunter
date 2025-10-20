using App.Code.Core.FSM;
using App.Code.Character.Player;

namespace App.Code.Character.Player.States
{
    public class PlayerCrouchState : IState
    {
        private PlayerController _playerController;

        public PlayerCrouchState(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void ExecutePhysics()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}