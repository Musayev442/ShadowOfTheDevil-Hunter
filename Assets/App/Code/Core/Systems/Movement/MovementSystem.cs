using System;
using System.Collections;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.Movement.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.Movement
{
    // ==================== MOVEMENT SYSTEM (COMPLETE WITH LOCK-ON) ====================
    public class MovementSystem : IMovementSystem
{
    private Rigidbody _rb;
    private Transform _transform;
    private Transform _cameraTransform;
    private MovementConfig _config;
    private Vector3 _currentVelocity;

    public MovementSystem(Rigidbody rigidbody, Transform transform, Transform cameraTransform, MovementConfig config)
    {
        _rb = rigidbody;
        _transform = transform;
        _cameraTransform = cameraTransform;
        _config = config;
        _currentVelocity = Vector3.zero;
    }

    public void Move(Vector3 input, float speed, bool allowMovement = true)
    {
        // Block movement during landing
        if (!allowMovement)
        {
            // Decelerate to stop
            _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, Time.fixedDeltaTime * _config.deceleration * 2f);
            _rb.linearVelocity = new Vector3(_currentVelocity.x, _rb.linearVelocity.y, _currentVelocity.z);
            return;
        }

        Vector3 cameraRelativeInput = GetCameraRelativeMovement(input);
        Vector3 targetVelocity = cameraRelativeInput * speed;

        float smoothFactor = (input.magnitude > 0.1f ? _config.acceleration : _config.deceleration) * Time.fixedDeltaTime;
        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, smoothFactor);

        _rb.linearVelocity = new Vector3(_currentVelocity.x, _rb.linearVelocity.y, _currentVelocity.z);

        Vector3 horizontalVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        if (horizontalVel.magnitude > _config.maxSpeed)
        {
            horizontalVel = horizontalVel.normalized * _config.maxSpeed;
            _rb.linearVelocity = new Vector3(horizontalVel.x, _rb.linearVelocity.y, horizontalVel.z);
        }
    }

    public void Rotate(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            Vector3 lookDirection = GetCameraRelativeMovement(direction);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    public void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - _transform.position);
        direction.y = 0; // Keep rotation on horizontal plane

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private Vector3 GetCameraRelativeMovement(Vector3 movementInput)
    {
        if (_cameraTransform == null) return movementInput;

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        return forward * movementInput.z + right * movementInput.x;
    }
}
}