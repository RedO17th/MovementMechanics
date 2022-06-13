using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : BaseController
{
    [Space]
    public float _fallSpeed = 3f;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    [Range(0f, 1f)]
    [SerializeField] private float _groundCheckerRadius;

    public bool IsGrounded
    {
        get => Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer, QueryTriggerInteraction.Ignore);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundChecker.position, _groundCheckerRadius);
    }
}
