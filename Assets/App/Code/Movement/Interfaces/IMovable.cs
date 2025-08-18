using UnityEngine;

public interface IMovable
{
    void Move(Vector3 direction, float speed);
    void RotateTowards(Vector3 direction);
}
