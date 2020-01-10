using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class NotMoveOnAttackSystem : ReactiveSystem<CoreEntity>
{
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Attack);

    public NotMoveOnAttackSystem(Contexts contexts) : base(contexts.core) {}

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(_matcher);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return true;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isEnableMove = !e.isAttack;
            
        }
    }
}
