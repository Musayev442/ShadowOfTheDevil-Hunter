using App.Code.Core.Input;
using App.Code.Core.Systems.AnimationSystem;
using App.Code.Core.Systems.Movement.Interfaces;
using App.Code.Core.Systems.Movement.Structs;

namespace App.Code.Core.Systems.Movement
{
    public class MovementSpeedState : IMovementSpeedState
    {
        private readonly MovementConfig _movementConfig;
        private readonly AnimationConfig _animationConfig;

        public MovementSpeedState(MovementConfig movementConfig, AnimationConfig animationConfig)
        {
            _movementConfig = movementConfig;
            _animationConfig = animationConfig;
        }

        public MovementStateData GetMovementState(IInputService input)
        {
            if (!input.HasMovementInput)
                return new MovementStateData(0f, _animationConfig.idleSpeed);
        
            if (input.IsSprinting)
                return new MovementStateData(_movementConfig.sprintSpeed, _animationConfig.sprintSpeed);
        
            if (input.IsWalking)
                return new MovementStateData(_movementConfig.walkSpeed, _animationConfig.walkSpeed);
        
            return new MovementStateData(_movementConfig.runSpeed, _animationConfig.runSpeed);
        }
    }
}