public class FastRunStratagy : MovementStratagy
{
    public FastRunStratagy(MovementController controller) : base(controller) { }

    protected override void DefineSpeed() => _speed = _movementController.MovementSettings.FastRunSpeed;
}