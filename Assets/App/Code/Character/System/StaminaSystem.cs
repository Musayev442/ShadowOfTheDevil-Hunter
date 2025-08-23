using Assets.App.Code.Character.System.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.System
{
    public class StaminaSystem : IStamina
    {
        public float Current { get; private set; }
        public float Max { get; private set; }

        public StaminaSystem(float maxStamina)
        {
            Max = maxStamina;
            Current = maxStamina;
        }

        public bool HasStamina(float amount) => Current >= amount;

        public void Consume(float amount)
        {
            Current = Mathf.Max(0, Current - amount);
        }

        public void Regenerate(float amount)
        {
            Current = Mathf.Min(Max, Current + amount);
        }
    }
}