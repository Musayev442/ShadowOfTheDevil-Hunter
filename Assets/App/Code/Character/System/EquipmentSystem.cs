using Assets.App.Code.Abstracts;
using SotD.Characters;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.System
{
    public class EquipmentSystem 
    {
        public Weapon CurrentWeapon { get; private set; }
        private SoulCharacter owner;

        public EquipmentSystem(SoulCharacter character)
        {
            owner = character;
        }
        public void EquipWeapon(Weapon weapon)
        {
            CurrentWeapon?.Unequip();
            CurrentWeapon = weapon;
            weapon.Equip(owner);
        }
    }
}