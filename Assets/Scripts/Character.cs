using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyOwnSandBox 
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;

        [Range(0f, 1f)]
        [SerializeField] private float _groundCheckerRadius;

        [SerializeField] private float _speedMovement;
        [SerializeField] private float _speedMovementAcceleration;

        [SerializeField] private float _speedRotation;

        [Header("Jump")]
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private AnimationCurve _smallJump;
        [SerializeField] private AnimationCurve _TestJump;

        [Header("Jerk")]
        [SerializeField] private float _jerkStep;

        private CharacterController _controller;

        private float _yJumpHeight = 0f;

        private bool _isJumped = false;

        private bool _isGrounded
        { 
            get => Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer, QueryTriggerInteraction.Ignore);
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {

            var horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal != 0f)
            { 
                Quaternion rotation = Quaternion.Euler(0f, horizontal * _speedRotation * Time.deltaTime, 0f);
                transform.rotation *= rotation;
            }

            _yJumpHeight += Physics.gravity.y * _fallSpeed * Time.deltaTime;

            if (_isGrounded && _yJumpHeight < 0)
                _yJumpHeight = -2f;

            var verticalAxis = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftControl) && verticalAxis != 0)
            {
                //Возможно перевести его на Lerp
                Vector3 jerkDirection = transform.forward * _jerkStep;

                _controller.Move(jerkDirection);
            }

            Vector3 direction = transform.forward * verticalAxis;

            float speed = (Input.GetKey(KeyCode.LeftShift)) ? _speedMovementAcceleration : _speedMovement;

            Vector3 moveVector = direction * speed * Time.deltaTime;
                    moveVector.y = _yJumpHeight * Time.deltaTime;

            if (!_isJumped && moveVector != Vector3.zero)
                _controller.Move(moveVector);

            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(TestJump(1f));
        }

        private IEnumerator TestJump(float duration)
        {
            _isJumped = true;

            float time = 0f;
            float mechanicProgress = 0f;

            float currentValue = 0f;
            float previousValue = 0f;

            //Дистанция корректна. Описать независимость от времени прыжка
            //Vector3 startPosition = transform.position;
            //..
            float wayForwardByJump = 3f;
            Vector3 distanceDirection = transform.forward * wayForwardByJump;

            //Vector3 prevPos = Vector3.zero;
            //Vector3 nextPos = Vector3.zero;

            //float distTraveled = 0f;

            while (mechanicProgress < 1)
            {
                time += Time.deltaTime;
                mechanicProgress = time / duration;

                var deltaJumpValue = 0f;
                currentValue = _TestJump.Evaluate(time);

                if (currentValue > previousValue)
                {
                    deltaJumpValue = currentValue - previousValue;
                    previousValue = currentValue;
                }
                else
                {
                    previousValue -= currentValue;
                    deltaJumpValue = previousValue * -1;

                    previousValue = currentValue;
                }

                //Если во время прыжка нажимать в сторону, то он туда же будет лететь
                //Баг ли это, или все таки фича...

                //nextPos = mechanicProgress * moveVector;
                //Vector3 movementDelta = nextPos - prevPos;
                //prevPos = nextPos;
                //distTraveled += movementDelta.magnitude;

                var moveVector = distanceDirection * Time.deltaTime;
                    moveVector.y = deltaJumpValue;

                //Ранее мы задали направление и некоторую delta, на которуюбудет изменяться положение
                _controller.Move(moveVector);

                yield return null;
            }

            //var distance = Vector3.Distance(startPosition, transform.position);
            //Debug.Log($"Character.TestJump: Пройденная дистанция во время прыжка = { distance } ");

            _isJumped = false;

            yield break;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_groundChecker.position, _groundCheckerRadius);
        }
    }   
}