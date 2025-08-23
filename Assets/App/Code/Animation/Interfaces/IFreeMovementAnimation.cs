using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation.Interfaces
{
    public interface IFreeMovementAnimation
    {
        void UpdateFreeMovementAnimation(Vector3 movement, bool isWalking);
    }
}