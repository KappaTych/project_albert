using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class DirectionComponent : IComponent
{
    public eMovement dir;
}
