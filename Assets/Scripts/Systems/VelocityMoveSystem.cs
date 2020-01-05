using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class VelocityMoveSystem : ReactiveSystem<CoreEntity>
{
    readonly IGroup<CoreEntity> _inputMove;

    public VelocityMoveSystem(Contexts contexts) : base(contexts.core)
    {
        _inputMove = contexts.core.GetGroup(CoreMatcher.Move);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(CoreMatcher.Move);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasMove && entity.hasMoveSpeed && entity.hasGameObject;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            var rb = e.gameObject.gameObj.GetComponent<Rigidbody2D>();

            var pos = e.move.movement;
            rb.velocity = pos * e.moveSpeed.speed;
        }
    }
}
