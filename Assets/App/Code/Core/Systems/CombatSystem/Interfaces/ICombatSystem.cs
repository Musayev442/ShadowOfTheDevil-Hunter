namespace App.Code.Core.Systems.CombatSystem.Interfaces
{
    public interface ICombatSystem
    {
        void Attack();                          // Request attack
        void UpdateAttack();                    // Update attack state
        bool IsAttacking();                     // Currently in attack
        int GetCurrentAttackIndex();            // Which attack in combo (0, 1, 2)
        bool CanMove();                         // Can player move
        bool CanRotate();
    }
}