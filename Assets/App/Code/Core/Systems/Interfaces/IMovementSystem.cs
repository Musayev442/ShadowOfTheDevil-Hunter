using System;
using UnityEngine;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IMovementSystem
    {
        float MoveSpeed { get; set; }
        bool IsMoving { get; }
        Vector3 CurrentVelocity { get; }

        void Move(Vector3 direction, float speed);
        void Rotate(Vector3 direction, Transform transform);
        void Stop();
    }
}