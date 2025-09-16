using Assets.App.Code.Character.System.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.System
{
    public class StaminaSystem 
    {
        public float MaxStamina { get; private set; }
        public float CurrentStamina { get; private set; }
        public float StaminaRegenRate { get; private set; }

        public event Action<float> OnStaminaChanged;

        public StaminaSystem(float maxStamina, float regenRate = 5f)
        {
            MaxStamina = maxStamina;
            CurrentStamina = MaxStamina;
            StaminaRegenRate = regenRate;
        }

        public bool ConsumeStamina(float amount)
        {
            if (CurrentStamina >= amount)
            {
                CurrentStamina -= amount;
                OnStaminaChanged?.Invoke(CurrentStamina);
                return true;
            }
            return false;
        }

        public void RegenerateStamina(float deltaTime)
        {
            CurrentStamina = Mathf.Min(MaxStamina, CurrentStamina + StaminaRegenRate * deltaTime);
            OnStaminaChanged?.Invoke(CurrentStamina);
        }

        public void RestoreFullStamina()
        {
            CurrentStamina = MaxStamina;
            OnStaminaChanged?.Invoke(CurrentStamina);
        }
    }
}