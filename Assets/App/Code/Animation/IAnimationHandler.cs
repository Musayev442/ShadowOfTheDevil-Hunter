using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation
{
    public interface IAnimationHandler
    {
        void SetMovementBlendTree(float speed, float horizontal, float vertical);
        void SetAnimationSpeed(float speed);
        void ResetAnimationStates();
    }

}