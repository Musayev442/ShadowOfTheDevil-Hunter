using Assets.App.Code.Animation.Interfaces;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterAnimation : ISprintAnimation, IFreeMovementAnimation
{
    private Animator animator;
    private float animatorSpeed;
    private float speedVelocity;
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

    public void UpdateFreeMovementAnimation(Vector3 movement, bool isWalking)
    {
        // Normalize animator speed (0 = idle, 0.4 = walk, 1 = run)
        float targetAnimatorSpeed = movement.magnitude * (isWalking ? 0.4f : 1f);
        animatorSpeed = Mathf.SmoothDamp(animatorSpeed, targetAnimatorSpeed, ref speedVelocity, 0.1f);

        // Update animator
        animator.SetFloat("Speed", animatorSpeed);

        //float smoothedSpeed = CalculateSmoothedSpeed(movement);
        //animator.SetFloat("Speed", smoothedSpeed);

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

    public void UpdateSprintAnimation(bool isSprinting)
    {
        animator.SetBool("isSprinting", isSprinting);
    }
}
