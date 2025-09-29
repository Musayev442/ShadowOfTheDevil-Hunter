using System;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IStaminaSystem
    {
        event Action<int> OnStaminaChanged;
        event Action OnStaminaDepleted;
    
        bool ConsumeStamina(float amount);
        void RegenerateStamina();
        float CurrentStamina { get; }
        float MaxStamina { get; }
    }
}