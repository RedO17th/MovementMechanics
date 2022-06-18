using MyOwnSandBox.EmptyCharacter;

public abstract class BaseRotationStratagy
{
    protected MovementController _movementController;

    protected Character _character;

    public BaseRotationStratagy(MovementController controller)
    {
        _movementController = controller;

        _character = _movementController.ControllerManager.Character;
    }

    public abstract void Rotate();
}


