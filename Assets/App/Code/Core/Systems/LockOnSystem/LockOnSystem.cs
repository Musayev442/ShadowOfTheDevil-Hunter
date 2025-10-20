using System.Collections.Generic;
using System.Linq;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.LockOnSystem
{
    public class LockOnSystem : ILockOnSystem
    {
        // Dependencies
        private Transform _playerTransform;
        private LockOnConfig _config;
    
        // State
        private bool _isLockedOn;
        private Transform _currentTarget;
        private List<Transform> _availableTargets;
    
        // Target switching cooldown
        private float _switchCooldownTimer;
        private float _currentDot;

        public LockOnSystem(Transform playerTransform, LockOnConfig config)
        {
            _playerTransform = playerTransform;
            _config = config;
        
            _isLockedOn = false;
            _currentTarget = null;
            _availableTargets = new List<Transform>();
            _switchCooldownTimer = 0f;
        }

        public void ToggleLockOn()
        {
            if (_isLockedOn)
            {
                DisableLockOn();
            }
            else
            {
                FindAndLockTarget();
            }
        }

        public void SwitchTarget(Vector2 direction)
        {
            // Check cooldown
            if (_switchCooldownTimer > 0f)
                return;
    
            if (!_isLockedOn || _availableTargets.Count <= 1)
                return;

            Transform newTarget = FindTargetInDirection(direction);
    
            if (newTarget != null && newTarget != _currentTarget)
            {
                _currentTarget = newTarget;
                _switchCooldownTimer = _config.switchCooldown;
                Debug.Log($"[LockOnSystem] Switched to: {_currentTarget.name}");
            }
        }

        public void UpdateLockOn()
        {
            // Update cooldown timer
            if (_switchCooldownTimer > 0f)
            {
                _switchCooldownTimer -= Time.deltaTime;
            }

            if (!_isLockedOn || _currentTarget == null)
                return;

            // Check if should break lock
            if (ShouldBreakLock())
            {
                DisableLockOn();
                return;
            }

            // Update available targets list
            UpdateAvailableTargets();
        }

        public bool IsLockedOn()
        {
            return _isLockedOn && _currentTarget != null;
        }

        public Transform GetCurrentTarget()
        {
            return _currentTarget;
        }

        private void DisableLockOn()
        {
            _isLockedOn = false;
            _currentTarget = null;
            _availableTargets.Clear();
    
            Debug.Log("[LockOnSystem] Lock-On DISABLED");
        }

        private void FindAndLockTarget()
        {
            // Clear the old list of targets
            _availableTargets.Clear();
            
            // Find all colliders in range
            Collider[] colliders = Physics.OverlapSphere(_playerTransform.position, _config.lockOnRadius,_config.targetLayer);
            
            // Filter by angle and tag
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    
                    // Check if target is within view angle
                    Vector3 directionToTarget = (collider.transform.position - _playerTransform.position).normalized;
                    float angle = Vector3.Angle(_playerTransform.forward, directionToTarget);

                    if (angle < _config.lockOnAngle)
                    {
                        _availableTargets.Add(collider.transform);
                    }
                }
            }
            
            // Step 3: Lock onto nearest target
            if (_availableTargets.Count > 0)
            {
                _currentTarget = GetNearestTarget();
                _isLockedOn = true;
                Debug.Log($"[LockOnSystem] Locked onto: {_currentTarget.name}");
            }
            else
            {
                Debug.Log("[LockOnSystem] No targets found in range!");
            }
        }
        
        
        private Transform GetNearestTarget()
        {
            Transform best = null;
            float bestScore = float.MaxValue;

            foreach (var target in _availableTargets)
            {
                // Calculate distance
                float distance = Vector3.Distance(_playerTransform.position, target.position);
        
                // Calculate angle from where player is looking
                Vector3 directionToTarget = (target.position - _playerTransform.position).normalized;
                float angle = Vector3.Angle(_playerTransform.forward, directionToTarget);
        
                // Weighted score: distance + angle weight
                // Lower score = better target
                float score = distance + (angle * 0.1f);  // Adjust 0.1f to change angle importance
        
                if (score < bestScore)
                {
                    bestScore = score;
                    best = target;
                }
            }

            return best;
        }

        private bool ShouldBreakLock()
        {
            if (_currentTarget == null)
                return true;

            // Check 1: Distance - too far?
            float distance = Vector3.Distance(_playerTransform.position, _currentTarget.position);
            if (distance > _config.breakDistance)
            {
                Debug.Log("[LockOnSystem] Target too far - breaking lock");
                return true;
            }

            // Check 2: Angle - target behind player?
            Vector3 directionToTarget = (_currentTarget.position - _playerTransform.position).normalized;
            float angle = Vector3.Angle(_playerTransform.forward, directionToTarget);
            if (angle > _config.breakAngle)
            {
                Debug.Log("[LockOnSystem] Target behind player - breaking lock");
                return true;
            }

            // Check 3: Obstacle blocking view?
            if (_config.breakOnObstacle)
            {
                if (Physics.Raycast(_playerTransform.position, directionToTarget, distance, _config.obstacleLayer))
                {
                    Debug.Log("[LockOnSystem] Obstacle blocking target - breaking lock");
                    return true;
                }
            }

            // Check 4: Target destroyed or inactive?
            if (!_currentTarget.gameObject.activeInHierarchy)
            {
                Debug.Log("[LockOnSystem] Target destroyed - breaking lock");
                return true;
            }

            return false;  // Keep lock-on
        }

        private void UpdateAvailableTargets()
        {
            _availableTargets.Clear();
    
            // Find all potential targets in range
            Collider[] colliders = Physics.OverlapSphere(
                _playerTransform.position, 
                _config.lockOnRadius, 
                _config.targetLayer
            );
    
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy") && collider.gameObject.activeInHierarchy)
                {
                    _availableTargets.Add(collider.transform);
                }
            }
        }

        private Transform FindTargetInDirection(Vector2 direction)
        {
            if (_currentTarget == null || _availableTargets.Count <= 1)
                return null;

            // Get camera right vector
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            // Sort all targets by their left-to-right position (relative to camera)
            List<Transform> sortedTargets = new List<Transform>(_availableTargets);
            sortedTargets.Sort((a, b) =>
            {
                Vector3 dirA = (a.position - _playerTransform.position);
                dirA.y = 0;
                float dotA = Vector3.Dot(dirA.normalized, cameraRight);
        
                Vector3 dirB = (b.position - _playerTransform.position);
                dirB.y = 0;
                float dotB = Vector3.Dot(dirB.normalized, cameraRight);
        
                return dotA.CompareTo(dotB);  // Sort left to right
            });

            // Find current target index in sorted list
            int currentIndex = sortedTargets.IndexOf(_currentTarget);
    
            if (currentIndex == -1)
                return null;

            // Switch RIGHT (Q key)
            if (direction.x > 0.5f)
            {
                if (currentIndex < sortedTargets.Count - 1)
                {
                    return sortedTargets[currentIndex + 1];  // Next target
                }
                else
                {
                    Debug.Log("[LockOnSystem] Already at rightmost target!");
                    return null;  // At edge
                }
            }
            // Switch LEFT (E key)
            else if (direction.x < -0.5f)
            {
                if (currentIndex > 0)
                {
                    return sortedTargets[currentIndex - 1];  // Previous target
                }
                else
                {
                    Debug.Log("[LockOnSystem] Already at leftmost target!");
                    return null;  // At edge
                }
            }

            return null;
        }
    }
}