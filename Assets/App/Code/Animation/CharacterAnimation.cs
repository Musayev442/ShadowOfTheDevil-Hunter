using UnityEngine;
using UnityEngine.Windows;

public class CharacterAnimation : ICharacterAnimation
{
    private Animator animator;
    public CharacterAnimation(Animator animator)
    {
        this.animator = animator;
    }
   
    private float CalculateSmoothedSpeed(Vector3 movement)
    {
        float speed = Mathf.Clamp01(movement.magnitude);
        float currentAnimSpeed = animator.GetFloat("Speed");
        return Mathf.MoveTowards(currentAnimSpeed, speed, Time.deltaTime * 5f);
    }

    public void UpdateFreeMovementAnimation(Vector3 movement)
    {
        float smoothedSpeed = CalculateSmoothedSpeed(movement);
        animator.SetFloat("Speed", smoothedSpeed);


        //bool isMoving = movement.magnitude > 0.1f;
        //animator.SetBool("isMoving", isMoving);
    }

    public void UpdateLockedOnMovementAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    public void UpdateJumpAnimation(Rigidbody rb, bool isJumping, bool isFalling, bool isGrounded)
    {
        //Debug.Log($"Jumping: {isJumping}, Grounded: {isGrounded}, Velocity Y: {rb.linearVelocity.y}");
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", false);
    }
}
