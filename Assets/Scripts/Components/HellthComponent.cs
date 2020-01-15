using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class HellthComponent : IComponent
{
    public int curValue;
    public int maxValue;
}

public partial class CoreEntity
{
    public void ReplaceHellth(int newCurValue)
    {
        ReplaceHellth(newCurValue, hellth?.maxValue ?? newCurValue);
    }
}