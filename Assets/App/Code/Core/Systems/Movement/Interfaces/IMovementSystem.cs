using UnityEngine;

namespace App.Code.Core.Systems.Movement.Interfaces
{
    public interface IMovementSystem
    {
        void Move(Vector3 input, float speed, bool allowMovement = true);
        void Rotate(Vector3 direction);
        void RotateTowardsTarget(Transform target);
    }
}