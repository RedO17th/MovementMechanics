using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyOwnSandBox.Character 
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private ControllerManager _controllerManager;

        //
        [SerializeField] private float _speedMovement;
        [SerializeField] private float _speedMovementAcceleration;

        [SerializeField] private float _speedRotation;

        //
        [Header("Jerk")]
        [SerializeField] private float _jerkStep;

        public CharacterController CharController { get; private set; }

        private float _yJumpHeight = 0f;

        private JumpController _jumpController;
        private GravityController _gravityController;

        private void Awake()
        {
            CharController = GetComponent<CharacterController>();

            _controllerManager.Initialize(this);

            _jumpController = (JumpController)_controllerManager.GetController(ControllerType.JumpController);
            _gravityController = (GravityController)_controllerManager.GetController(ControllerType.GravityController);
        }

        private void Update()
        {

            var horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal != 0f)
            { 
                Quaternion rotation = Quaternion.Euler(0f, horizontal * _speedRotation * Time.deltaTime, 0f);
                transform.rotation *= rotation;
            }

            _yJumpHeight += Physics.gravity.y * _gravityController._fallSpeed * Time.deltaTime;

            if (_gravityController.IsGrounded && _yJumpHeight < 0)
                _yJumpHeight = -2f;

            var verticalAxis = Input.GetAxisRaw("Vertical");

            //if (Input.GetKeyDown(KeyCode.LeftControl) && verticalAxis != 0)
            //{
            //    Vector3 jerkDirection = transform.forward * _jerkStep;
            //    CharController.Move(jerkDirection);
            //}

            Vector3 direction = transform.forward * verticalAxis;

            float speed = (Input.GetKey(KeyCode.LeftShift)) ? _speedMovementAcceleration : _speedMovement;

            Vector3 moveVector = direction * speed * Time.deltaTime;
                    moveVector.y = _yJumpHeight * Time.deltaTime;

            if (!_jumpController.IsJumped && moveVector != Vector3.zero)
                CharController.Move(moveVector);
        }
    }   
}