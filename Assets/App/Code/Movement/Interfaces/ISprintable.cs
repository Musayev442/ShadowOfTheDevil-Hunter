using UnityEditor;
using UnityEngine;

namespace App.Code.Movement.Interfaces
{
    public interface ISprintable
    {
        bool CanSprint { get; }
        void Sprint(Vector3 direction);
    }
}