using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Event(EventTarget.Self)]
public class AttackTypeComponent : IComponent
{
   public eAttackType t;
}
