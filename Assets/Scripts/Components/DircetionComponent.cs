using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class DirectionComponent : IComponent
{
    public int dir;
}
