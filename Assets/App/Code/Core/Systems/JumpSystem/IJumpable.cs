namespace App.Code.Core.Systems.Interfaces
{
    public interface IJumpable
    {
        void Jump();
        void UpdateJump();
        bool IsGrounded();
        bool HasJustJumped(); 
    }
}