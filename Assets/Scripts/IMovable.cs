using UnityEngine;

public interface IMovable
{
    Vector3 ForwardDirection { get; }
    Vector3 Position { get; }
    void Move(Vector3 direction);
}
