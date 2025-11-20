using System;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IStaminaSystem
    {
       float CurrentStamina { get; }
        float MaxStamina { get; }
        bool IsRegenerating { get; }
        
        event Action<float, float> OnStaminaChanged;  // (current, max)
        event Action OnStaminaDepleted;
        
        void UseStamina(float amount);
        void RegenerateStamina(float deltaTime);
        void SetStamina(float amount);
        bool HasEnoughStamina(float amount);
    }
}