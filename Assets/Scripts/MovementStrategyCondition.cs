using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategyCondition
{
    public abstract bool CheckCondition();
    public abstract void Log();
}
