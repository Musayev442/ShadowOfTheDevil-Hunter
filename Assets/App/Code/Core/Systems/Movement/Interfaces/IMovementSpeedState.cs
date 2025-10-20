using App.Code.Core.Input;
using App.Code.Core.Systems.Movement.Structs;

namespace App.Code.Core.Systems.Movement.Interfaces
{
    public interface IMovementSpeedState
    {
        MovementStateData GetMovementState(IInputService input);
    }
}