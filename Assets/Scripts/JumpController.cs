using MyOwnSandBox.EmptyCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : BaseController
{
    [Space]
    [Header("Jump")]
    [Range(1f, 2f)]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private AnimationCurve _smallJump;
    [SerializeField] private AnimationCurve _TestJump;

    public bool IsJumped { get; private set; } = false;

    //private CharacterController _charController;
    //private GravityController _gravityController;

    //Здесь будут размещаться несколько типо прыжков - полиморфизировать
    //small jump
    //medium jump
    //big jump
    //Скорее всего необходимо добавить интерфейс IJumpable и передавать данный интерфейс в текущий контроллер

    public override void Initialize(ControllerManager manager)
    {
        base.Initialize(manager);

        //_charController = ControllerManager.Character.CharController;
        //_gravityController = (GravityController)ControllerManager.GetController(ControllerType.GravityController);
    }

    private void Update()
    {
        //if (_gravityController.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        //    StartCoroutine(TestJump(1f));
    }

    //private IEnumerator TestJump(float duration)
    //{
    //    IsJumped = true;

    //    float time = 0f;
    //    float mechanicProgress = 0f;

    //    float currentYValue = 0f;
    //    float previousYValue = 0f;

    //    float wayForwardByJump = 3f;
    //    Vector3 distanceDirection = transform.forward * wayForwardByJump;

    //    while (mechanicProgress < 1)
    //    {
    //        time += Time.deltaTime;
    //        mechanicProgress = time / duration;

    //        var deltaJumpValue = 0f;
    //        currentYValue = _TestJump.Evaluate(time);

    //        if (currentYValue > previousYValue)
    //        {
    //            deltaJumpValue = currentYValue - previousYValue;
    //            previousYValue = currentYValue;
    //        }
    //        else
    //        {
    //            previousYValue -= currentYValue;
    //            deltaJumpValue = previousYValue * -1;

    //            previousYValue = currentYValue;
    //        }

    //        var moveVector = distanceDirection * Time.deltaTime;
    //            moveVector.y = deltaJumpValue * _jumpHeight;

    //        _charController.Move(moveVector);

    //        yield return null;
    //    }

    //    IsJumped = false;
    //}
}

public abstract class BaseJumpStratagy
{
    protected JumpController _jumpController;
    protected Character _character;

    public bool IsJumped { get; protected set; } = false;

    public BaseJumpStratagy(JumpController jumpController)
    {
        _jumpController = jumpController;
        _character = jumpController.ControllerManager.Character;
    }

    public abstract void Jump();
}

public class SmallJumpStratagy : BaseJumpStratagy
{
    private GravityController _gravityController;

    public SmallJumpStratagy(JumpController jumpController) : base(jumpController)
    {
        _gravityController = (GravityController)_jumpController.ControllerManager.GetController(ControllerType.GravityController);
    }

    public override void Jump()
    {
        _jumpController.StartCoroutine(JumpRealisation(1f));
    }

    private IEnumerator JumpRealisation(float duration)
    {
        IsJumped = true;

        float time = 0f;
        float mechanicProgress = 0f;

        float currentYValue = 0f;
        float previousYValue = 0f;

        float wayForwardByJump = 3f;
        Vector3 distanceDirection = transform.forward * wayForwardByJump;

        while (mechanicProgress < 1)
        {
            time += Time.deltaTime;
            mechanicProgress = time / duration;

            var deltaJumpValue = 0f;
            currentYValue = _TestJump.Evaluate(time);

            if (currentYValue > previousYValue)
            {
                deltaJumpValue = currentYValue - previousYValue;
                previousYValue = currentYValue;
            }
            else
            {
                previousYValue -= currentYValue;
                deltaJumpValue = previousYValue * -1;

                previousYValue = currentYValue;
            }

            var moveVector = distanceDirection * Time.deltaTime;
                moveVector.y = deltaJumpValue * _jumpHeight;

            _charController.Move(moveVector);

            yield return null;
        }

        IsJumped = false;
    }
}
