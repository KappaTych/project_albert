using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class AttackComponent :  IComponent {
    public bool active;
}
