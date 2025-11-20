using App.Code.Core.Input;
using App.Code.Core.Systems.CameraSystem.Interfaces;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.CameraSystem
{
// ==================== CAMERA SYSTEM ====================

    public class CameraSystem : ICameraSystem
    {
        // ==================== FIELDS ====================

        // Dependencies
        private Camera _camera;
        private Transform _cameraTransform;
        private CameraConfig _config;
        private IInputService _inputService;
        private ILockOnSystem _lockOnSystem;

        // Camera state
        private float _currentZoom;
        private float _targetRotation;
        private float _currentRotation;
        private Vector3 _lockOnPositionOffset;

        // ==================== CONSTRUCTOR ====================

        public CameraSystem(Camera camera, CameraConfig config, ILockOnSystem lockOnSystem, IInputService inputService)
        {
            _camera = camera;
            _cameraTransform = camera.transform;
            _config = config;
            _lockOnSystem = lockOnSystem;
            _inputService = inputService;

            _currentZoom = config.cameraHeight;
            _currentRotation = 0f;
            _targetRotation = 0f;
            _lockOnPositionOffset = Vector3.zero;
        }

        // Main coordinator - delegates to specific methods
        public void UpdateCamera(Transform player)
        {
            if (_cameraTransform == null || player == null)
                return;

            HandleZoom();
            HandleRotation();
            HandleLockOn(player);
            UpdateCameraPosition(player);
            UpdateCameraLookAt(player);
        }

        private void HandleLockOn(Transform player)
        {
            if (!_config.enableLockOnCamera)
            {
                _lockOnPositionOffset = Vector3.zero;
                return;
            }

            if (!_lockOnSystem.IsLockedOn())
            {
                // Smoothly reset offset when not locked on
                _lockOnPositionOffset = Vector3.Lerp(
                    _lockOnPositionOffset,
                    Vector3.zero,
                    Time.deltaTime * _config.lockOnTransitionSpeed
                );
                return;
            }

            Transform target = _lockOnSystem.GetCurrentTarget();
            if (target == null)
            {
                _lockOnPositionOffset = Vector3.zero;
                return;
            }

            // Calculate midpoint between player and target
            Vector3 midPoint = (player.position + target.position) / 2f;

            // Calculate offset to shift camera toward action
            Vector3 targetOffset = (midPoint - player.position) * 0.5f; // Shift 50% toward midpoint
            targetOffset.y = 0; // Keep on horizontal plane

            // Smooth transition to target offset
            _lockOnPositionOffset = Vector3.Lerp(
                _lockOnPositionOffset,
                targetOffset,
                Time.deltaTime * _config.lockOnTransitionSpeed
            );
        }

        // Zoom functionality
        private void HandleZoom()
        {
            float scrollInput = _inputService.ScrollInput;
            _currentZoom -= scrollInput * _config.zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, _config.minZoom, _config.maxZoom);
        }

        // Rotation functionality
        private void HandleRotation()
        {
            bool rotateLeft = _inputService.RotateCameraLeft;
            bool rotateRight = _inputService.RotateCameraRight;

            if (rotateLeft)
                _targetRotation += _config.rotationAngle;
            if (rotateRight)
                _targetRotation -= _config.rotationAngle;

            // ✅ WRAP target rotation to 0-360 range
            _targetRotation = NormalizeAngle(_targetRotation);

            // Smooth rotation
            _currentRotation =
                Mathf.LerpAngle(_currentRotation, _targetRotation, Time.deltaTime * _config.rotationSpeed);

            // ✅ WRAP current rotation to 0-360 range
            _currentRotation = NormalizeAngle(_currentRotation);
        }

        private float NormalizeAngle(float angle)
        {
            angle = angle % 360f;
            if (angle < 0)
                angle += 360f;
            return angle;
        }

        // Position update functionality
        private void UpdateCameraPosition(Transform player)
        {
            Vector3 desiredPosition = CalculateDesiredPosition(player);
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, desiredPosition,
                Time.deltaTime * _config.followSpeed);
        }

        // Look-at functionality
        private void UpdateCameraLookAt(Transform player)
        {
            Vector3 lookAtPosition = player.position;
    
            // ✅ Look at midpoint between player and target when locked on
            if (_lockOnSystem != null && _lockOnSystem.IsLockedOn())
            {
                Transform target = _lockOnSystem.GetCurrentTarget();
                if (target != null)
                {
                    lookAtPosition = (player.position + target.position) / 2f;
                    lookAtPosition.y = target.position.y + 1f;  // Look at target's upper body
                }
            }
    
            _cameraTransform.LookAt(lookAtPosition);
        }

        // Position calculation
        private Vector3 CalculateDesiredPosition(Transform player)
        {
            // Calculate zoom with lock-on multiplier
            float effectiveZoom = _currentZoom;
            float effectiveDistance = _config.cameraDistance;
    
            //  Zoom out when locked on to show both characters
            if (_lockOnSystem != null && _lockOnSystem.IsLockedOn())
            {
                effectiveZoom *= _config.lockOnDistanceMultiplier;
                effectiveDistance *= _config.lockOnDistanceMultiplier;
                effectiveZoom += _config.lockOnHeightOffset;
            }
    
            float ratio = effectiveZoom / _config.cameraHeight;
            Vector3 offset = new Vector3(0, effectiveZoom, -effectiveDistance * ratio);
    
            // Apply tilt
            Quaternion tilt = Quaternion.Euler(_config.cameraAngle, 0, 0);
            offset = tilt * offset;
    
            // Apply rotation
            Quaternion rotation = Quaternion.Euler(0, _currentRotation, 0);
            offset = rotation * offset;
    
            // lock-on position offset
            return player.position + offset + _lockOnPositionOffset;
        }
    }
}