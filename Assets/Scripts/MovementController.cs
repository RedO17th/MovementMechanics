using MyOwnSandBox.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : BaseController
{
    //Idle, Walk, Run, Fast Run, Jerk, Rotation - Состояния, которые должен контролировать данный контроллер

    [SerializeField] private MovementSettings _movementSettings;

    //
    [SerializeField] private float _speedRotation;
    //

    public MovementSettings MovementSettings => _movementSettings;

    private Dictionary<MovementStrategyCondition, BaseMovementStratagy> movementStrategies = null;

    public override void Initialize(ControllerManager manager)
    {
        base.Initialize(manager);

        movementStrategies = new Dictionary<MovementStrategyCondition, BaseMovementStratagy>()
        {
            {  new WalkStrategyCondition(), new WalkStratagy(this) },
            {  new RunStrategyCondition(), new RunStratagy(this) },
            {  new FastRunStrategyCondition(), new FastRunStratagy(this) },
            {  new JerkStrategyCondition(), new JerkStratagy(this) }
        };
    }

    private void Update()
    {
        #region
        //var horizontal = Input.GetAxisRaw("Horizontal");
        //if (horizontal != 0f)
        //{
        //    Quaternion rotation = Quaternion.Euler(0f, horizontal * _speedRotation * Time.deltaTime, 0f);
        //    transform.rotation *= rotation;
        //}
        #endregion

        foreach (var strategyPair in movementStrategies)
        {
            if (strategyPair.Key.CheckCondition())
            {
                strategyPair.Key.Log();
                strategyPair.Value.Move();
            }
        }
    }

}

[System.Serializable]
public class MovementSettings
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _fastRunSpeed;
    [SerializeField] private float _jerkSpeed;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float FastRunSpeed => _fastRunSpeed;
    public float JerkSpeed => _jerkSpeed;
}

public abstract class MovementStrategyCondition
{ 
    public abstract bool CheckCondition();
    public abstract void Log();
}

public class WalkStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = run && Input.GetKey(KeyCode.LeftAlt);
        var jerk = Input.GetKeyDown(KeyCode.LeftControl);

        return movement && !run && !fastRun && !jerk;
    }
    public override void Log()
    {
        Debug.Log($"WalkStrategyCondition");
    }
}

public class RunStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = Input.GetKey(KeyCode.LeftAlt);

        return movement && run && !fastRun;
    }
    public override void Log()
    {
        Debug.Log($"RunStrategyCondition");
    }
}

public class FastRunStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = run && Input.GetKey(KeyCode.LeftAlt);

        return movement && fastRun;
    }
    public override void Log()
    {
        Debug.Log($"FastRunStrategyCondition");
    }
}

public class JerkStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.LeftControl);
    }
    public override void Log()
    {
        Debug.Log($"JerkStrategyCondition");
    }
}

public interface IMovable
{
    void Move();
}

public abstract class BaseMovementStratagy : IMovable
{
    protected MovementController _movementController;

    protected Character _character;
    protected CharacterController _charController;

    public BaseMovementStratagy(MovementController controller)
    {
        _movementController = controller;

        _character = _movementController.ControllerManager.Character;
        _charController = _character.CharController;
    }

    public abstract void Move();
}

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

        _charController.Move(jerk);
    }
}

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
        ToMove();
    }

    protected virtual void DefineSpeed() => _speed = _movementController.MovementSettings.WalkSpeed;

    protected virtual void ToMove()
    {
        var direction = _character.ForwardDirection * Input.GetAxisRaw("Vertical");
        var moveVector = direction * _speed * Time.deltaTime;
            moveVector.y = _gravityController.YPositionByGravity * Time.deltaTime;

        if (!_jumpController.IsJumped && moveVector != Vector3.zero)
            _charController.Move(moveVector);
    }
}

public class WalkStratagy : MovementStratagy
{
    public WalkStratagy(MovementController controller) : base(controller) { }
}

public class RunStratagy : MovementStratagy
{
    public RunStratagy(MovementController controller) : base(controller) { }

    protected override void DefineSpeed() => _speed = _movementController.MovementSettings.RunSpeed;
}

public class FastRunStratagy : MovementStratagy
{
    public FastRunStratagy(MovementController controller) : base(controller) { }

    protected override void DefineSpeed() => _speed = _movementController.MovementSettings.FastRunSpeed;
}