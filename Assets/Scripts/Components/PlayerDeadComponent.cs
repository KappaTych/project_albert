using Entitas;
using Entitas.CodeGeneration.Attributes;


[Core, Unique, Event(EventTarget.Any)]
public sealed class PlayerDeadComponent : IComponent {}
