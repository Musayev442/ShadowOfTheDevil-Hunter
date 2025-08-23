using Assets.App.Code.Character.System.Interfaces;
using UnityEngine;

namespace Assets.App.Code.Character.System
{
    public class HealthSystem : IHealth
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public delegate void HealthChanged(float current, float max);
        public event HealthChanged OnHealthChanged;

        public delegate void Death();
        public event Death OnDeath;

        public HealthSystem(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (IsDead) return;

            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            if (IsDead) return;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void Die()
        {
            if (IsDead)
            {
                OnDeath?.Invoke();
                // Optional: trigger death animation, ragdoll, or respawn
            }
        }
    }

}
