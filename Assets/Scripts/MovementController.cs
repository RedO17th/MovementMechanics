using MyOwnSandBox.EmptyCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : BaseController
{
    [SerializeField] private MovementSettings _movementSettings;

    public MovementSettings MovementSettings => _movementSettings;

    private BaseRotationStratagy _rotationStratagy;
    private Dictionary<MovementStrategyCondition, BaseMovementStratagy> movementStrategies = null;

    public override void Initialize(ControllerManager manager)
    {
        base.Initialize(manager);

        _rotationStratagy = new EmptyRotationStratagy(this);
        movementStrategies = new Dictionary<MovementStrategyCondition, BaseMovementStratagy>()
        {
            {  new WalkStrategyCondition(), new WalkStratagy(this) },
            {  new RunStrategyCondition(), new RunStratagy(this) },
            {  new FastRunStrategyCondition(), new FastRunStratagy(this) }
        };
    }

    private void Update()
    {
        Rotation();
        Move();
    }

    private void Rotation() => _rotationStratagy.Rotate();
    private void Move()
    {
        foreach (var strategyPair in movementStrategies)
        {
            if (strategyPair.Key.CheckCondition())
            {
                //strategyPair.Key.Log();
                strategyPair.Value.Move();
            }
        }
    }
}


