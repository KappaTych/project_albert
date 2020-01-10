using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class VelocityMoveSystem : IExecuteSystem
{
    readonly IGroup<CoreEntity> _inputMove;
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Move, CoreMatcher.GameObject, CoreMatcher.MoveSpeed);

    public VelocityMoveSystem(Contexts contexts)
    {
        _inputMove = contexts.core.GetGroup(_matcher);
    }

    public void Execute()
    {
        foreach (var e in _inputMove.GetEntities())
        {
            var rb = e.gameObject.gameObj.GetComponent<Rigidbody2D>();

            var pos = e.move.movement;
            rb.velocity = pos * e.moveSpeed.speed;
            if (e.isEnableMove)
                rb.velocity = Vector2.zero;
        }
    }
}
