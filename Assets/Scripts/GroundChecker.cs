using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _groundLayer;

    public Vector3 Position => transform.position;
    public float Radius => _groundCheckerRadius;

    public bool AtGround
    {
        get { return Physics.CheckSphere(Position, Radius, _groundLayer, QueryTriggerInteraction.Ignore); }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Position, Radius);
    }
}
