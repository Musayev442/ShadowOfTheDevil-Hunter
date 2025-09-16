using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts
{
    [System.Serializable]
    public class Status
    {
        public int CurrentHealth;
        public float CurrentStamina;
        public bool IsAlive => CurrentHealth > 0;
        public bool IsInvulnerable = false;
        public bool IsStaggered = false;
        public bool IsLockedOn = false;
    }
}