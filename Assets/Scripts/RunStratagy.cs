public class RunStratagy : MovementStratagy
{
    public RunStratagy(MovementController controller) : base(controller) { }

    protected override void DefineSpeed() => _speed = _movementController.MovementSettings.RunSpeed;
}
