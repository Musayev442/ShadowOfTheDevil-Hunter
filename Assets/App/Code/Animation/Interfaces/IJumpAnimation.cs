using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation.Interfaces
{
    public interface IJumpAnimation
    {
        void PlayJumpStart();
        void PlayJumpFalling();
        void PlayJumpEnd();
        bool IsJumping { get; }
    }
}