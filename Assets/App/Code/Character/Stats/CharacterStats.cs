using UnityEngine;

namespace App.Code.Character.Stats
{
    // Character Stats System
    [System.Serializable]
    public struct CharacterStats
    {
        public int Level;         // Increase the Level to upgrade Character Stats
        public int Vitality;     // Increases Max Health
        public int Endurance;    // Increases Max Stamina and Physical Defense
        public int Strength;     // Scales Physical Damage
        public int Dexterity;    // Scales Dexterity-based weapons and crit chance
        public int Intelligence; // Scales Magic Damage and Magic Defense
        public int Faith;        // For future expansion (e.g., Miracles)

        public int CurrentHealth;
        public int MaxHealth => 100 + (Vitality * 10);
        public int CurrentStamina;
        public int MaxStamina => 50 + (Endurance * 5);
        public int CurrentMana;
        public int MaxMana => 30 + (Intelligence * 8);
    
        // Derived stats
        public int PhysicalDamage => 10 + (Strength * 3);
        public int MagicDamage => 5 + (Intelligence * 4);
        public int PhysicalDefense => Endurance * 2;
        public int MagicDefense => Intelligence * 1;
        public int CriticalChance => Mathf.Min(5 + (Dexterity * 2), 50); // Max 50% crit
        public float MovementSpeed => 5f + (Dexterity * 0.1f);
        public float StaminaRegenRate => 10f + (Endurance * 2f);
        public float ManaRegenRate => 5f + (Intelligence * 1.5f);
    
        // Constructor for initial stats
        public CharacterStats(int level, int vit, int end, int str, int dex, int intel, int faith)
        {
            Level = level;
            Vitality = vit;
            Endurance = end;
            Strength = str;
            Dexterity = dex;
            Intelligence = intel;
            Faith = faith;
        
            CurrentHealth = 100 + (vit * 10);
            CurrentStamina = 50 + (end * 5);
            CurrentMana = 30 + (intel * 8);
        }
    }
}