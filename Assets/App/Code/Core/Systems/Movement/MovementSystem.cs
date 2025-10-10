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
        private Vector3 _currentVelocity;
        private Quaternion _targetRotation;

        public float MoveSpeed { get; set; }
        public bool IsMoving { get; }
        public Vector3 CurrentVelocity { get; }

        public MovementSystem(Rigidbody rigidbody, Transform cameraTransform)
        {
            _rb = rigidbody;
            _cameraTransform = cameraTransform;
        }

        public void Move(Vector3 direction, float moveSpeed)
        {
            if (direction.magnitude > 0.1f)
            {
                // Convert input direction to camera-relative direction
                Vector3 cameraRelativeMovement = GetCameraRelativeMovement(direction);
            
                // Apply smooth movement using velocity
                Vector3 targetVelocity = cameraRelativeMovement * moveSpeed;
                targetVelocity.y = _rb.linearVelocity.y; // Preserve gravity
            
                _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * 10f);
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