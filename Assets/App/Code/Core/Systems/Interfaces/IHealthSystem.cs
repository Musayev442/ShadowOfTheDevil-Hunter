using System;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IHealthSystem
    {
        event Action OnDeath;
        event Action<int> OnHealthChanged;
        event Action<int> OnDamageTaken;
    
        void TakeDamage(float damage);
        void Heal(float amount);
        bool IsAlive { get; }
        float CurrentHealth { get; }
        float MaxHealth { get; }
    }
}