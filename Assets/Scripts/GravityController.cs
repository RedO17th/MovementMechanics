using MyOwnSandBox.EmptyCharacter;
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

    public float YPositionByGravity { get; private set; }

    public bool IsGrounded
    {
        get => Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void Update()
    {
        YPositionByGravity += Physics.gravity.y * _fallSpeed * Time.deltaTime;

        if (IsGrounded && YPositionByGravity < 0)
            YPositionByGravity = -2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundChecker.position, _groundCheckerRadius);
    }
}
