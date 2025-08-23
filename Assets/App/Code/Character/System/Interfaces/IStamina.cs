using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.System.Interfaces
{
    public interface IStamina
    {
        float Current { get; }
        float Max { get; }
        bool HasStamina(float amount);
        void Consume(float amount);
        void Regenerate(float amount);
    }
}