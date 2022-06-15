using MyOwnSandBox.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected ControllerType _controllerType;

    public ControllerType ControllerType => _controllerType;

    public ControllerManager ControllerManager { get; private set; }

    public virtual void Initialize(ControllerManager manager)
    {
        ControllerManager = manager;
    }
}
