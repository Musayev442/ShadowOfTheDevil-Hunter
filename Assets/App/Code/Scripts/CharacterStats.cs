using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts
{
    [System.Serializable]
    public class CharacterStats
    {
        public int Level = 1;
        public int Vitality = 10;
        public int Endurance = 10;
        public int Strength = 10;
        public int Dexterity = 10;
        public int Intelligence = 10;
        public int Faith = 10;

        public int MaxHealth => Vitality * 10;
        public float MaxStamina => Endurance * 10f;
        public float MaxEquipLoad => Endurance * 15f;
    }
}