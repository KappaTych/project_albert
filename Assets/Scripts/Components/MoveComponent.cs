using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Core,  Event(EventTarget.Self)]
public class MoveComponent : IComponent
{
    public Vector2 movement;
}