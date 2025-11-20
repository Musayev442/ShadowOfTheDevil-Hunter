using UnityEngine;

namespace App.Code.Core.Systems.CameraSystem
{
    [System.Serializable]
    public class CameraConfig
    {
        [Header("Camera Position")]
        public float cameraHeight = 15f;        // Default height above player
        public float cameraDistance = 10f;      // Distance back from player
        public float cameraAngle = 45f;         // Fixed look-down angle
    
        [Header("Zoom")]
        public float minZoom = 8f;              // Closest zoom
        public float maxZoom = 20f;             // Furthest zoom
        public float zoomSpeed = 5f;            // Mouse wheel sensitivity
    
        [Header("Rotation")]
        public float rotationAngle = 90f;       // Degrees per Q/E press
        public float rotationSpeed = 8f;        // Smooth rotation speed
    
        [Header("Follow")]
        public float followSpeed = 10f;         // How fast camera follows player
        
        [Header("Lock-On")]
        public bool enableLockOnCamera = true;           // Toggle lock-on camera feature
        public float lockOnTransitionSpeed = 5f;         // Speed of camera adjustment
        public float lockOnDistanceMultiplier = 1.3f;    // Zoom out multiplier (1.3 = 30% further)
        public float lockOnHeightOffset = 2f;            // Additional height when locked on
    }
}