using UnityEngine;

public interface ICharacterAnimation
{
    
    void UpdateLockedOnMovementAnimation(float horizontal, float vertical);
    void UpdateJumpAnimation(Rigidbody rb, bool isJumping, bool isFalling, bool isGrounded);
}
