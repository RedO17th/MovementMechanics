using UnityEngine;

public class WalkStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = run && Input.GetKey(KeyCode.LeftAlt);
        var jerk = Input.GetKeyDown(KeyCode.LeftControl);

        return movement && !run && !fastRun && !jerk;
    }
    public override void Log()
    {
        Debug.Log($"WalkStrategyCondition");
    }
}
