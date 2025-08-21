using UnityEngine;

public interface IPlayerInput
{
    float GetHorizontalInput();
    float GetVerticalInput();
    bool GetJumpInput();
    bool GetSprintInput();
    bool GetWalkInput();
}
