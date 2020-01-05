using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class Hellth : IComponent
{
    public float curValue;
    public float maxValue;
}

public partial class CoreEntity
{
    public void ReplaceHellth(float newCurValue)
    {
        ReplaceHellth(newCurValue, hellth?.maxValue ?? newCurValue);
    }
}