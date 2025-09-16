using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts.Data
{
    public struct DamageData
    {
        public int BaseDamage;
        public DamageType DamageType;
        public Vector3 HitDirection;
        public bool IsCritical;
        public bool CanBeParried;
    }
}