using App.Code.Movement.Interfaces;
using SotD.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : IMovable
{
    private Rigidbody _rb;
    private Vector3 _velocity;
    private float acceleration = 10f;

    public Movement(Rigidbody rb, float acceleration)
    {
        this._rb = rb;
        this.acceleration = acceleration;
    }

    public void Move(Vector3 direction, float targetSpeed)
    {
        // Smooth velocity
        _velocity = Vector3.MoveTowards(
           new Vector3(_velocity.x, 0, _velocity.z),
           direction * targetSpeed,
           acceleration * Time.fixedDeltaTime
        );

        _rb.linearVelocity = new Vector3(_velocity.x, _rb.linearVelocity.y, _velocity.z);
    }

    public void RotateTowards(Vector3 direction)
    {
        // Normalize to avoid scaling issues
        if (direction.sqrMagnitude < 1e-6f) return;
        direction.Normalize();

        // Rotation (face forward)
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Optional: smooth turning (feels nicer in Souls-like)
        Quaternion smoothed = Quaternion.RotateTowards(
            _rb.rotation,
            targetRotation,
            720f * Time.fixedDeltaTime // turning speed in degrees/sec
        );

        _rb.MoveRotation(smoothed);
    }

}
