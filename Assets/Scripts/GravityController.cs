using MyOwnSandBox.EmptyCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : BaseController
{
    [Space]
    [SerializeField] float _fallSpeed = 3f;

    [SerializeField] private GroundChecker _groundChecker;

    public float YPositionByGravity { get; private set; }

    public bool IsGrounded => _groundChecker.AtGround;

    private void Update()
    {
        YPositionByGravity += Physics.gravity.y * _fallSpeed * Time.deltaTime;

        if (IsGrounded && YPositionByGravity < 0)
            YPositionByGravity = -2f;
    }
}
