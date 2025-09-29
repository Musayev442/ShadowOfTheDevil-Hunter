using Assets.App.Code.Character.Player;
using Assets.App.Code.Core.FSM;

namespace App.Code.Character.Player.States
{
    public class PlayerSprintState : IState
    {
        private PlayerController _playerController;

        public PlayerSprintState(PlayerController playerController)
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