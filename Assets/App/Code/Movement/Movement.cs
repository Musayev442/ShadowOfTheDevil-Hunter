using App.Code.Movement.Interfaces;
using SotD.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : IMovable
{
    private Rigidbody rb;
    public Movement(Rigidbody rb)
    {
        this.rb = rb;
    }

    public void Move(Vector3 direction, float speed)
    {
        // Position
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        Vector3 newPos = rb.position + movement;
        rb.MovePosition(newPos);
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
            rb.rotation,
            targetRotation,
            720f * Time.fixedDeltaTime // turning speed in degrees/sec
        );

        rb.MoveRotation(smoothed);
    }
}
