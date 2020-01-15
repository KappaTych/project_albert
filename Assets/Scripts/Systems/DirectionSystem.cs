using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class DirectionSystem : ReactiveSystem<CoreEntity>
{
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Move);

    public DirectionSystem(Contexts contexts) : base(contexts.core) {}

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(_matcher);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasMove && entity.move.isMoving();
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            var mov = e.move.movement;
            e.ReplaceDirection(MovementExtensions.GetCounterClockDirection(mov));
        }
    }
}
