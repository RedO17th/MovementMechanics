using UnityEngine;

public class JerkStrategyCondition
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.LeftControl);
    }
    public void Log()
    {
        Debug.Log($"JerkStrategyCondition");
    }
}
