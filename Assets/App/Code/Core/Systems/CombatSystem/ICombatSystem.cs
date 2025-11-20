using App.Code.Character.Base;

namespace App.Code.Core.Systems.Interfaces
{
    public interface ICombatSystem
    {
        void Attack(SoulCharacter target, float damage);
        void Block();
        void Dodge();
        bool IsBlocking { get; }
    }
}