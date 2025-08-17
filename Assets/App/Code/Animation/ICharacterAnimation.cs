using UnityEngine;

public interface ICharacterAnimation
{
    void UpdateLockedOnMovementAnimation(float horizontal, float vertical);
    void UpdateFreeMovementAnimation(Vector3 movement);
}
