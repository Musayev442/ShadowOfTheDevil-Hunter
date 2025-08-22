using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float acceleration = 10f;

    [Header("References")]
    public Rigidbody rb;
    public Animator animator;

    private Vector3 inputDirection;
    private float currentSpeed;
    private float animatorSpeed;
    private float speedVelocity;

    void Update()
    {
        // Input direction
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(h, 0, v).normalized;

        // Walk override
        bool isWalking = Input.GetKey(KeyCode.LeftControl);
        float targetSpeed = isWalking ? walkSpeed : runSpeed;

        // Smooth movement speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed * inputDirection.magnitude, acceleration * Time.deltaTime);

        // Normalize animator speed (0 = idle, 0.4 = walk, 1 = run)
        float targetAnimatorSpeed = inputDirection.magnitude * (isWalking ? 0.4f : 1f);
        animatorSpeed = Mathf.SmoothDamp(animatorSpeed, targetAnimatorSpeed, ref speedVelocity, 0.1f);

        // Update animator
        animator.SetFloat("Speed", animatorSpeed);
    }

    void FixedUpdate()
    {
        // Apply movement
        Vector3 velocity = inputDirection * currentSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        // Rotate toward movement direction
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
}
