using UnityEngine;

namespace App.Code.Core.Systems.LockOnSystem
{
    [System.Serializable]
    public class LockOnConfig
    {
        [Header("Lock-On Detection")]
        public float lockOnRadius = 15f;           // How far to search for targets
        public float lockOnAngle = 90f;            // Field of view for lock-on
        public LayerMask targetLayer;              // What can be locked (Enemy layer)
    
        [Header("Lock-On Breaking")]
        public float breakDistance = 20f;          // Distance to break lock
        public float breakAngle = 120f;            // Angle behind player to break lock
        public bool breakOnObstacle = true;        // Break if obstacle blocks view
        public LayerMask obstacleLayer;            // What blocks lock (walls, etc.)
    
        [Header("Target Switching")]
        public float switchCooldown = 0.2f;        // Cooldown between target switches
    
        [Header("Camera")]
        public float cameraLerpSpeed = 5f;         // How fast camera follows target
        public Vector3 cameraOffset = new Vector3(0, 1.5f, 0);  // Offset from target center
        
        
    }
}