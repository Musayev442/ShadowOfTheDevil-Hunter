using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.System.Interfaces
{
    public interface IHealth
    {
        void TakeDamage(float amount);
        void Heal(float amount);
        void Die();
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsDead { get; }
    }
}