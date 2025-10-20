namespace App.Code.Core.FSM
{
        public interface IState
        {
            void Enter();
            void ExecuteUpdate();
            void ExecutePhysics();
            void Exit();
        }
}