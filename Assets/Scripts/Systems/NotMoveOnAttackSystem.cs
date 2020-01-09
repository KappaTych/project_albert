using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class NotMoveOnAttackSystem : ReactiveSystem<CoreEntity>
{
    readonly IGroup<CoreEntity> _inputMove;
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Move, CoreMatcher.GameObject);

    public NotMoveOnAttackSystem(Contexts contexts) : base(contexts.core)
    {
        _inputMove = contexts.core.GetGroup(_matcher);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(_matcher);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasMove && entity.hasGameObject;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isEnableMove = !e.isAttack;
            
        }
    }
}
