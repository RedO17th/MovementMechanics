using MyOwnSandBox.EmptyCharacter;
using UnityEngine;

public abstract class BaseMovementStratagy
{
    protected MovementController _movementController;

    protected Character _character;

    public BaseMovementStratagy(MovementController controller)
    {
        _movementController = controller;

        _character = _movementController.ControllerManager.Character;
    }

    public abstract void Move();
}
