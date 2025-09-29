using System;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IManaSystem
    {
        event Action<int> OnManaChanged;
        event Action OnManaDepleted;
    
        bool ConsumeMana(float amount);
        void RegenerateMana();
        float CurrentMana { get; }
        float MaxMana { get; }
    }
}