using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class ManaComponent : IComponent
{
    public int curValue;
    public int maxValue;
}

public partial class CoreEntity
{
    public void ReplaceMana(int newCurValue)
    {
        ReplaceMana(newCurValue, mana?.maxValue ?? newCurValue);
    }
}