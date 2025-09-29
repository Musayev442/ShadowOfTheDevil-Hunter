using System;
using UnityEngine;
using App.Code.Core.Systems.Interfaces;

namespace App.Code.Core.Systems.StaminaSystem
{
    [System.Serializable]
    public class StaminaSystem
    {
        [SerializeField] private int maxStamina = 100;
        [SerializeField] private int currentStamina;
        [SerializeField] private float regenRate = 10f;
        [SerializeField] private float regenDelay = 1f;
        [SerializeField] private bool isRegenerating = true;

        // Properties
        public int CurrentStamina => currentStamina;
        public int MaxStamina => maxStamina;

        // Events
        public event Action<int> OnStaminaChanged;
        public event Action OnStaminaDepleted;

        private float timeSinceLastUse;
        private string ownerName;

        public StaminaSystem(string ownerName = "Character")
        {
            this.ownerName = ownerName;
            currentStamina = maxStamina;
        }

        public void Update(float deltaTime)
        {
            if (isRegenerating)
            {
                RegenerateStamina(deltaTime);
            }
        }

        public bool CanPerformAction(int staminaCost)
        {
            return currentStamina >= staminaCost;
        }

        public bool ConsumeStamina(int amount)
        {
            if (!CanPerformAction(amount))
            {
                if (currentStamina <= 0)
                {
                    OnStaminaDepleted?.Invoke();
                }

                return false;
            }

            currentStamina -= amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            timeSinceLastUse = 0f;

            OnStaminaChanged?.Invoke(currentStamina);
            Debug.Log($"{ownerName} used {amount} stamina. Stamina: {currentStamina}/{maxStamina}");

            if (currentStamina <= 0)
            {
                OnStaminaDepleted?.Invoke();
            }

            return true;
        }

        public void RestoreStamina(int amount)
        {
            if (amount <= 0) return;

            currentStamina += amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            OnStaminaChanged?.Invoke(currentStamina);
        }

        public void SetMaxStamina(int newMaxStamina)
        {
            float staminaPercentage = maxStamina > 0 ? (float)currentStamina / maxStamina : 1f;
            maxStamina = newMaxStamina;
            currentStamina = Mathf.RoundToInt(newMaxStamina * staminaPercentage);
            OnStaminaChanged?.Invoke(currentStamina);
        }

        public void SetRegenRate(float newRegenRate)
        {
            regenRate = newRegenRate;
        }

        private void RegenerateStamina(float deltaTime)
        {
            timeSinceLastUse += deltaTime;

            if (currentStamina < maxStamina && timeSinceLastUse >= regenDelay)
            {
                int regenAmount = Mathf.RoundToInt(regenRate * deltaTime);
                RestoreStamina(regenAmount);
            }
        }

        public void SetRegenerating(bool regenerating)
        {
            isRegenerating = regenerating;
        }
    }
}