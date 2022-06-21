using MyOwnSandBox.EmptyCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : BaseController
{
    [SerializeField] private JumpSettings _jumpSettings;
    public JumpSettings JumpSettings => _jumpSettings;

    public bool IsJumped => _jumpStratagy.IsJumped;

    private GravityController _gravityController;

    private BaseJumpStratagy _jumpStratagy = null;

    public override void Initialize(ControllerManager manager)
    {
        base.Initialize(manager);

        _gravityController = (GravityController)ControllerManager.GetController(ControllerType.GravityController);

        _jumpStratagy = new SmallJumpStratagy(this);
    }

    private void Update()
    {
        if (_gravityController.IsGrounded && Input.GetKeyDown(KeyCode.Space))
            _jumpStratagy.Jump();

        

    }
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
    public abstract void StopJump();
}

public class SmallJumpStratagy : BaseJumpStratagy
{
    private float _jumpHeight;
    private float _jumpTime;
    private AnimationCurve _jumpCurve;

    private Coroutine _jumpRoutine;

    public SmallJumpStratagy(JumpController jumpController) : base(jumpController)
    {
        _jumpCurve = _jumpController.JumpSettings.JumpCurve;
        _jumpHeight = _jumpController.JumpSettings.JumpHeight;
        _jumpTime = _jumpController.JumpSettings.JumpTime;
    }

    public override void Jump()
    {
        _jumpRoutine = _jumpController.StartCoroutine(JumpRealisation());
    }

    public override void StopJump()
    {
        _jumpController.StopCoroutine(_jumpRoutine);
        _jumpRoutine = null;

        IsJumped = false;
    }

    private IEnumerator JumpRealisation()
    {
        IsJumped = true;
        Debug.Log($"JumpController.JumpRealisation.StartJump");

        float time = 0f;
        float mechanicProgress = 0f;

        float currentYValue = 0f;
        float previousYValue = 0f;

        //[1]
        //Здесь игрок будет двигаться вперед в течении wayForwardByJump [REFACT] - Тоже не верно, 
        //_jumpTime должен регулировать время всей "анимации", а не просто сдвиг вперед
        float jumpStepToforward = _jumpTime;
        var jumpDistance = _character.ForwardDirection * jumpStepToforward;
        //..

        while (mechanicProgress < 1)
        {
            //Есть прогресс механики через ее время
            time += Time.deltaTime;
            mechanicProgress = time / _jumpTime;
            //Но в Curve сам прыжок заканчивается раньше... Нужно сделать так, чтобы и сам прыжок заканчивался соответственно mechanicProgress
            //currentYValue = _jumpCurve.Evaluate(time);
            currentYValue = _jumpCurve.Evaluate(mechanicProgress);

            var deltaJumpValue = 0f;
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

            //[1]
            //var moveVector = jumpDistance * Time.deltaTime;
            var moveVector = Vector3.zero;
                moveVector.y = deltaJumpValue * _jumpHeight;
            //..

            _character.Jump(moveVector);

            yield return null;
        }

        Debug.Log($"JumpController.JumpRealisation.EndJump");
        IsJumped = false;
    }
}


[System.Serializable]
public class JumpSettings
{
    [SerializeField] private string _nameJumpStratagy;

    [Range(1f, 2f)]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpTime;
    [SerializeField] private AnimationCurve _jumpCurve;

    public float JumpHeight => _jumpHeight;
    public float JumpTime => _jumpTime;
    public AnimationCurve JumpCurve => _jumpCurve;
}
