using Assets.App.Code.Scripts.Core.Interfaces;
using Assets.App.Code.Scripts.Data;
using SotD.Characters;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Abstracts
{
    public abstract class Weapon : IEquipable
    {
        public string WeaponName;
        public int BaseDamage;
        public DamageType DamageType;
        public float Weight;
        public bool CanParry = true;
        public int RequiredStrength = 10;
        public int RequiredDexterity = 10;

        protected SoulCharacter owner;

        public virtual void Equip(SoulCharacter character)
        {
            owner = character;
        }

        public virtual void Unequip()
        {
            owner = null;
        }

        public abstract void ExecuteAttack(AttackType attackType);
        public abstract float GetStaminaCost(AttackType attackType);

        protected bool CheckStatRequirements()
        {
            return owner.stats.Strength >= RequiredStrength &&
                   owner.stats.Dexterity >= RequiredDexterity;
        }
    }

    public class StraightSword : Weapon
    {
        public override void ExecuteAttack(AttackType attackType)
        {
            if (!CheckStatRequirements()) return;
            // Attack logic would be handled by animation events
        }

        public override float GetStaminaCost(AttackType attackType)
        {
            return attackType switch
            {
                AttackType.Light => 10f,
                AttackType.Heavy => 20f,
                AttackType.Jump => 15f,
                AttackType.Dash => 12f,
                _ => 10f
            };
        }
    }
}