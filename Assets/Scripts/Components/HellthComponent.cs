using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class Hellth : IComponent
{
    public float curValue;
    public float maxValue;
}
