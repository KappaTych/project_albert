using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class ClampMoveSystem : ReactiveSystem<CoreEntity>
{
    readonly IGroup<CoreEntity> _inputMove;

    public ClampMoveSystem(Contexts contexts) : base(contexts.core)
    {
        _inputMove = contexts.core.GetGroup(CoreMatcher.Move);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(CoreMatcher.Move);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasMove && entity.hasMoveSpeed && entity.hasGameObject && entity.isEnableMove;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            var rb = e.gameObject.gameObj.GetComponent<Rigidbody2D>();

            var input = Vector2.ClampMagnitude(e.move.movement, 1);
            var movement = input * e.moveSpeed.speed;

            var newPos = rb.position + movement * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
