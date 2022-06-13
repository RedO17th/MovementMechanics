using MyOwnSandBox.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : BaseController
{
    [Header("Jump")]
    [Range(1f, 2f)]
    [SerializeField] private float _jumpHeight;
    public float _fallSpeed;
    [SerializeField] private AnimationCurve _smallJump;
    [SerializeField] private AnimationCurve _TestJump;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    [Range(0f, 1f)]
    [SerializeField] private float _groundCheckerRadius;

    public bool IsGrounded
    {
        get => Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer, QueryTriggerInteraction.Ignore);
    }

    public bool IsJumped { get; private set; } = false;

    private CharacterController _charController;

    //Здесь будут размещаться несколько типо прыжков - полиморфизировать
    //weak jump
    //medium jump
    //big jump
    //Скорее всего необходимо добавить интерфейс IJumpable и передавать данный интерфейс в текущий контроллер

    public override void Initialize(ControllerManager manager)
    {
        base.Initialize(manager);

        _charController = _controllerManager.Character.CharController;
    }

    private void Update()
    {
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
            ExecuteMechanic();
    }

    public override void ExecuteMechanic()
    {
        StartCoroutine(TestJump(1f));
    }

    private IEnumerator TestJump(float duration)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundChecker.position, _groundCheckerRadius);
    }
}
