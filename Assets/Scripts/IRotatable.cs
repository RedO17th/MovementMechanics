using UnityEngine;

namespace MyOwnSandBox.EmptyCharacter
{
    public interface IRotatable
    {
        Quaternion Rotation { get; }
        void Rotate(Quaternion rotation);
    }
}