using UnityEngine;

public class JerkStratagy : BaseMovementStratagy
{
    private float _jerkSpeed = 5f;

    public JerkStratagy(MovementController controller) : base(controller) 
    {
        _jerkSpeed = controller.MovementSettings.JerkSpeed;
    }

    public override void Move()
    {
        float movementDirection = Input.GetAxisRaw("Vertical");
        Vector3 jerkStep = _character.ForwardDirection * _jerkSpeed;

        Vector3 jerk = (movementDirection != 0) ? movementDirection * jerkStep : jerkStep;

        _character.Move(jerk);
    }
}
