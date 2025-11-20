using UnityEngine;
using UnityEngine.UI;

namespace App.Code.UI.HUD.Views
{
    public class StaminaBarView
    {
        [SerializeField] private Image staminaFillBar;
        
        public void UpdateStaminaBar(float currentStamina, float maxStamina)
        {
            staminaFillBar.fillAmount = currentStamina / maxStamina;
        }
    }
}