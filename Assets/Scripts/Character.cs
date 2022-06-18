using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyOwnSandBox.EmptyCharacter
{
    public class Character : MonoBehaviour, IMovable, IRotatable, IJumpable
    {
        [SerializeField] private ControllerManager _controllerManager;

        public CharacterController CharController { get; private set; }
        public Vector3 ForwardDirection => transform.forward;
        public Quaternion Rotation => transform.rotation;

        private void Awake()
        {
            CharController = GetComponent<CharacterController>();

            _controllerManager.Initialize(this);
        }

        public void Move(Vector3 direction)
        {
            CharController.Move(direction);
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void Jump(Vector3 direction)
        {
            CharController.Move(direction);
        }
    }  
}