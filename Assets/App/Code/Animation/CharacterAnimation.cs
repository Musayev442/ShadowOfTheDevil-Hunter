using UnityEngine;
using UnityEngine.Windows;

public class CharacterAnimation : ICharacterAnimation
{
    private Animator animator;
    public CharacterAnimation(Animator animator)
    {
        this.animator = animator;
    }
    public void UpdateFreeMovementAnimation(Vector3 movement)
    {
        float speed = Mathf.Clamp01(movement.magnitude);
        float currentAnimSpeed = animator.GetFloat("Speed");
        float smoothedSpeed = Mathf.MoveTowards(currentAnimSpeed, speed, Time.deltaTime * 5f);
        animator.SetFloat("Speed", smoothedSpeed);


        //bool isMoving = movement.magnitude > 0.1f;
        //animator.SetBool("isMoving", isMoving);
    }

    public void UpdateLockedOnMovementAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
}
