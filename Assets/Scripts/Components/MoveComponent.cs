using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Core,  Event(EventTarget.Self)]
public class MoveComponent : IComponent
{
    public Vector2 movement;

    public bool isMoving() { return movement.magnitude > 0.01f; }
}