using Assets.App.Code.StateMachine;
using SotD.Characters.Player;

namespace Assets.App.Code.Character.Player.States
{
    public class PlayerAttackState : IState
    {
        PlayerController player;
        public PlayerAttackState(PlayerController player)
        {
            this.player = player;  
        }
        public void Enter()
        {
            throw new global::System.NotImplementedException();
        }

        public void Execute()
        {
            throw new global::System.NotImplementedException();
        }

        public void ExecutePhysics()
        {
            throw new global::System.NotImplementedException();
        }

        public void Exit()
        {
            throw new global::System.NotImplementedException();
        }
    }
}