using Assets.App.Code.Character.System;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts.Core.Interfaces
{
    public interface IStaminaUser 
    {
        StaminaSystem StaminaSystem { get; }
        bool ConsumeStamina(float amount);
    }
}