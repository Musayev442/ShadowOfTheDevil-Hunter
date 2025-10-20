using UnityEngine;

namespace App.Code.Core.Systems.CombatSystem
{
    public class CombatConfig
    {
        [Header("Attack Timing")]
        public float attack1Duration = 0.6f;
        public float attack2Duration = 0.7f;
        public float attack3Duration = 0.8f;
    
        [Header("Combo Window")]
        public float comboWindowStart = 0.4f;  // When you can input next attack
        public float comboWindowEnd = 0.8f;    // Last chance to continue combo
    
        [Header("Cooldown")]
        public float attackCooldown = 0.3f;    // Time after attack before can attack again
    
        [Header("Movement")]
        public bool blockMovementDuringAttack = true;
        public bool blockRotationDuringAttack = true;
    }
}