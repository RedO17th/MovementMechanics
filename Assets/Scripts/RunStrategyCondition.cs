using UnityEngine;

public class RunStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = Input.GetKey(KeyCode.LeftAlt);

        return movement && run && !fastRun;
    }
    public override void Log()
    {
        Debug.Log($"RunStrategyCondition");
    }
}
