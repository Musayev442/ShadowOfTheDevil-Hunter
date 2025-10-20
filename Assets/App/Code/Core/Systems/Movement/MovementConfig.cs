using UnityEngine;

namespace App.Code.Core.Systems.Movement
{
    [System.Serializable]
    public class MovementConfig
    {
        [Header("Movement Speeds")]
        public float walkSpeed = 2f;
        public float runSpeed = 5f;      // Default movement
        public float sprintSpeed = 8f;
    
        [Header("Movement Parameters")]
        public float acceleration = 10f;
        public float deceleration = 15f;
        public float maxSpeed = 10f;
    }
}