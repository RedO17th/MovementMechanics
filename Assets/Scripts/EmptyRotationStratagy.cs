using UnityEngine;

public class EmptyRotationStratagy : BaseRotationStratagy
{
    public EmptyRotationStratagy(MovementController controller) : base(controller) { }

    public override void Rotate() 
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var rotation = _character.Rotation;
            rotation *= Quaternion.Euler(0f, horizontal * _movementController.MovementSettings.RotationSpeed * Time.deltaTime, 0f);

        _character.Rotate(rotation);
    }
}


