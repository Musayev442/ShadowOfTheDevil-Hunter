using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation
{
    public interface IAnimationHandler
    {
        void Initialize(Animator animator);
        void UpdateAnimationState();
        void SetFloat(string parameter, float value);
        void SetBool(string parameter, bool value);
        void SetTrigger(string parameter);
        void ResetTrigger(string parameter);
    }
}