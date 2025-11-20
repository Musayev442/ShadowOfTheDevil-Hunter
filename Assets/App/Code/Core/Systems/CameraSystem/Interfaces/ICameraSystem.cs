using App.Code.Core.Input;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.CameraSystem.Interfaces
{
    public interface ICameraSystem
    {
        void UpdateCamera(Transform player);
    }
}