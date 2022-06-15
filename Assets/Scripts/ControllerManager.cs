using MyOwnSandBox.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyOwnSandBox.Character
{ 
    public enum ControllerType { None = -1, JumpController, GravityController, MovementController }

    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private List<BaseController> _conrollersList;

        public Character Character { get; private set; }

        public void Initialize(Character character)
        {
            Character = character;

            InitializeControllers();
        }

        private void InitializeControllers()
        {
            foreach (var controller in _conrollersList)
                controller.Initialize(this);
        }

        public BaseController GetController(ControllerType type)
        {
            BaseController controller = null;

            foreach (var item in _conrollersList.Where(x => x.ControllerType == type))
                controller = item;

            if (controller == null)
                Debug.Log($"ControllerManager.GetController: Necessary controller is empty or not exist");

            return controller;
        }
    }
}

