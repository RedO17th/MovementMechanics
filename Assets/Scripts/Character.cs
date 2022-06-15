using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyOwnSandBox.Character 
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private ControllerManager _controllerManager;

        public CharacterController CharController { get; private set; }
        public Vector3 ForwardDirection => transform.forward;

        private void Awake()
        {
            CharController = GetComponent<CharacterController>();

            _controllerManager.Initialize(this);
        }
    }   
}