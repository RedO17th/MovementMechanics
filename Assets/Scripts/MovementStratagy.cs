using MyOwnSandBox.EmptyCharacter;
using UnityEngine;

public abstract class MovementStratagy : BaseMovementStratagy
{
    protected JumpController _jumpController;
    protected GravityController _gravityController;

    protected float _speed = 0f;

    public MovementStratagy(MovementController controller) : base(controller)
    {
        _jumpController = (JumpController)_movementController.ControllerManager.GetController(ControllerType.JumpController);
        _gravityController = (GravityController)_movementController.ControllerManager.GetController(ControllerType.GravityController);
    }

    public override void Move() 
    {
        DefineSpeed();
        MoveRealisation();
    }

    protected virtual void DefineSpeed() => _speed = _movementController.MovementSettings.WalkSpeed;

    protected virtual void MoveRealisation()
    {
        var direction = _character.ForwardDirection * Input.GetAxisRaw("Vertical");
        var moveVector = direction * _speed * Time.deltaTime;
            moveVector.y = _gravityController.YPositionByGravity * Time.deltaTime;

        if (!_jumpController.IsJumped && moveVector != Vector3.zero)
            _character.Move(moveVector);
    }
}
