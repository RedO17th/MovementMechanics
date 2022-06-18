using UnityEngine;

public class FastRunStrategyCondition : MovementStrategyCondition
{
    public override bool CheckCondition()
    {
        var movement = Input.GetAxisRaw("Vertical") != 0f;
        var run = Input.GetKey(KeyCode.LeftShift);
        var fastRun = run && Input.GetKey(KeyCode.LeftAlt);

        return movement && fastRun;
    }
    public override void Log()
    {
        Debug.Log($"FastRunStrategyCondition");
    }
}
