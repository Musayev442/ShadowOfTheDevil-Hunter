using System.Collections;
using UnityEngine;
using SotD.Characters;


namespace Assets.App.Code.Scripts.Core.Interfaces
{
    public interface IEquipable
    {
        void Equip(SoulCharacter character);
        void Unequip();
    }
}