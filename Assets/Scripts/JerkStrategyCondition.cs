using UnityEngine;

public class JerkStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.LeftControl);
    }
    public override void Log()
    {
        Debug.Log($"JerkStrategyCondition");
    }
}
