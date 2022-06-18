using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementSettings
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _fastRunSpeed;
    [SerializeField] private float _jerkSpeed;
    [SerializeField] private float _rotationSpeed;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float FastRunSpeed => _fastRunSpeed;
    public float JerkSpeed => _jerkSpeed;
    public float RotationSpeed => _rotationSpeed;
}
