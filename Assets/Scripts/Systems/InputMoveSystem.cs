using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class InputMoveSystem : ReactiveSystem<CoreEntity>
{
    readonly IGroup<CoreEntity> _inputMove;

    public InputMoveSystem(Contexts contexts) : base(contexts.core)
    {
        _inputMove = contexts.core.GetGroup(CoreMatcher.InputMove);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(CoreMatcher.InputMove);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasInputMove && entity.hasMoveSpeed && entity.hasGameObject;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            var rb = e.gameObject.gameObj.GetComponent<Rigidbody2D>();

            var pos = e.inputMove.movement;
            rb.velocity = pos * e.moveSpeed.speed;
        }
    }
}
