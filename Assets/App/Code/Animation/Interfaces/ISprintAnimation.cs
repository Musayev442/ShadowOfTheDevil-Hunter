using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation.Interfaces
{
    public interface ISprintAnimation
    {
        void PlaySprint();
        void StopSprint();
        bool IsSprinting { get; }
    }
}