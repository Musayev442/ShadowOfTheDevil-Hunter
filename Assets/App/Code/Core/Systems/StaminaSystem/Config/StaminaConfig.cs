using UnityEngine;

namespace App.Code.Core.Systems.StaminaSystem.Config
{
    [System.Serializable]
    public class StaminaConfig
    {
        [Header("Stamina Settings")]
        [SerializeField] private float maxStamina = 100f;
        
        [Header("Regeneration")]
        [SerializeField] private float regenRate = 20f;
        [SerializeField] private float regenDelay = 1f;
        
        [Header("Stamina Costs")]
        [SerializeField] private float dodgeCost = 25f;
        [SerializeField] private float sprintCostPerSecond = 10f;
        [SerializeField] private float lightAttackCost = 15f;
        [SerializeField] private float heavyAttackCost = 30f;
        
        public float MaxStamina => maxStamina;
        public float RegenRate => regenRate;
        public float RegenDelay => regenDelay;
        public float DodgeCost => dodgeCost;
        public float SprintCostPerSecond => sprintCostPerSecond;
        public float LightAttackCost => lightAttackCost;
        public float HeavyAttackCost => heavyAttackCost;
    }
}