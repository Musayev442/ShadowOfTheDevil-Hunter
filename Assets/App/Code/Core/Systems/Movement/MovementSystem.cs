using System;
using System.Collections;
using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.Movement
{
    public class MovementSystem : IMovementSystem
    {
        private readonly Rigidbody _rb;
        private readonly Transform _cameraTransform;
        [Header("Smooth Movement")]
        public float acceleration = 10f;
        public float deceleration = 10f;
        public float maxSpeed = 8f;

        private Vector3 _currentVelocity;

        public MovementSystem(Rigidbody rigidbody, Transform cameraTransform)
        {
            _rb = rigidbody;
            _cameraTransform = cameraTransform;
        }


        public void Move(Vector3 moveInput, float moveSpeed)
        {
            // Calculate target velocity
            Vector3 targetVelocity = moveInput * moveSpeed;
        
            // Smoothly interpolate towards target velocity using fixedDeltaTime
            float smoothFactor = (moveInput.magnitude > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime;
            _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, smoothFactor);
        
            // Apply movement while preserving Y velocity for jumping/gravity
            _rb.linearVelocity = new Vector3(_currentVelocity.x, _rb.linearVelocity.y, _currentVelocity.z);
        
            // Optional: Limit maximum horizontal speed
            Vector3 horizontalVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            if (horizontalVel.magnitude > maxSpeed)
            {
                horizontalVel = horizontalVel.normalized * maxSpeed;
                _rb.linearVelocity = new Vector3(horizontalVel.x, _rb.linearVelocity.y, horizontalVel.z);
            }
        }
        
        public void Rotate(Vector3 direction, Transform transform)
        {
            if (direction.magnitude > 0.1f)
            {
                Vector3 lookDirection = GetCameraRelativeMovement(direction);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
            }
        }

        public void Stop()
        {
            Vector3 velocity = _rb.linearVelocity;
            velocity.x = 0;
            velocity.z = 0;
            _rb.linearVelocity = velocity;
        }

        public Vector3 GetCameraRelativeMovement(Vector3 movementInput)
        {
            Vector3 forward = _cameraTransform.forward;
            Vector3 right = _cameraTransform.right;
        
            // Remove Y component and normalize
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();
        
            return forward * movementInput.z + right * movementInput.x;
        }
        
    }
}