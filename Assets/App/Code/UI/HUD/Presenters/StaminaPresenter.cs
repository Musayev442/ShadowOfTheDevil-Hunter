using App.Code.Core.Systems.Interfaces;
using App.Code.UI.HUD.Views;
using UnityEngine;

namespace App.Code.UI.HUD.Presenters
{
    public class StaminaPresenter
    {
        [SerializeField] private StaminaBarView _staminaView;
        private IStaminaSystem _staminaSystem;
        
        public void Initialize(IStaminaSystem staminaSystem)
        {
            this._staminaSystem = staminaSystem;
        }
        
        private void OnEnable()
        {
            if (_staminaSystem != null)
            {
                _staminaSystem.OnStaminaChanged += HandleStaminaChanged;
                _staminaSystem.OnStaminaDepleted += HandleStaminaDepleted;
            }
        }
        
        private void OnDisable()
        {
            if (_staminaSystem != null)
            {
                _staminaSystem.OnStaminaChanged -= HandleStaminaChanged;
                _staminaSystem.OnStaminaDepleted -= HandleStaminaDepleted;
            }
        }
        
        private void HandleStaminaChanged(float currentStamina, float maxStamina)
        {
            _staminaView.UpdateStaminaBar(currentStamina, maxStamina);
        }
        
        private void HandleStaminaDepleted()
        {
            // You can add visual feedback here later (flash red, etc.)
            Debug.Log("Stamina depleted!");
        }
    }
}