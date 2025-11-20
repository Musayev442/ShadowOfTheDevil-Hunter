using System;
using UnityEngine;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.StaminaSystem.Config;

namespace App.Code.Core.Systems.StaminaSystem
{
    public class StaminaSystem : IStaminaSystem
    {
        private float _currentStamina;
        private float _maxStamina;
        private float _regenRate;
        private float _regenDelay;
        private float _regenDelayTimer;
        
        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _maxStamina;
        public bool IsRegenerating => _regenDelayTimer <= 0f;
        
        public event Action<float, float> OnStaminaChanged;
        public event Action OnStaminaDepleted;
        
        // Constructor now takes config
        public StaminaSystem(StaminaConfig config)
        {
            _maxStamina = config.MaxStamina;
            _currentStamina = _maxStamina;
            _regenRate = config.RegenRate;
            _regenDelay = config.RegenDelay;
            _regenDelayTimer = 0f;
        }
        
        public void UseStamina(float amount)
        {
            if (amount <= 0) return;
            
            _currentStamina = Mathf.Max(0, _currentStamina - amount);
            _regenDelayTimer = _regenDelay;
            
            OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
            
            if (_currentStamina <= 0)
            {
                OnStaminaDepleted?.Invoke();
            }
        }
        
        public void RegenerateStamina(float deltaTime)
        {
            if (_regenDelayTimer > 0)
            {
                _regenDelayTimer -= deltaTime;
                return;
            }
            
            if (_currentStamina >= _maxStamina)
                return;
            
            _currentStamina = Mathf.Min(_maxStamina, _currentStamina + _regenRate * deltaTime);
            OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
        }
        
        public void SetStamina(float amount)
        {
            _currentStamina = Mathf.Clamp(amount, 0, _maxStamina);
            OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
        }
        
        public bool HasEnoughStamina(float amount)
        {
            return _currentStamina >= amount;
        }
    
    }
}