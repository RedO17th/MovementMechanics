using MyOwnSandBox.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : BaseController
{
    [Space]
    [SerializeField] float _fallSpeed = 3f;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    [Range(0f, 1f)]
    [SerializeField] private float _groundCheckerRadius;

    public float YPositionByGravity => _yJumpHeight;

    private float _yJumpHeight = 0f;

    public bool IsGrounded
    {
        get => Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void Update()
    {
        _yJumpHeight += Physics.gravity.y * _fallSpeed * Time.deltaTime;

        if (IsGrounded && _yJumpHeight < 0)
            _yJumpHeight = -2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundChecker.position, _groundCheckerRadius);
    }
}
