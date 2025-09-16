using Assets.App.Code.Character.System;
using Assets.App.Code.Scripts.Data;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts.Core.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(DamageData damageData);
        void Die();
        bool IsAlive { get; }
        HealthSystem HealthSystem { get; }
    }
}