using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public sealed class CollisionComponent : IComponent
{
    public int otherEntityId;
}
