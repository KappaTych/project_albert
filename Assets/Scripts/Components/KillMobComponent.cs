using Entitas;
using Entitas.CodeGeneration.Attributes;


[Core, Event(EventTarget.Self)]
public class KillMobComponent : IComponent
{
    public int count;
    public int exp;
}
