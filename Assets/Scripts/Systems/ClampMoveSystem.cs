using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class ClampMoveSystem : IExecuteSystem
{
    readonly IGroup<CoreEntity> _inputMove;
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Move, CoreMatcher.GameObject, 
                                                               CoreMatcher.MoveSpeed);

    public ClampMoveSystem(Contexts contexts)
    {
        _inputMove = contexts.core.GetGroup(_matcher);
    }

    public void Execute()
    {
        foreach (var e in _inputMove.GetEntities())
        {
            if (!e.isEnableMove)
                continue;
            
            var rb = e.gameObject.gameObj.GetComponent<Rigidbody2D>();

            var input = Vector2.ClampMagnitude(e.move.movement, 1);
            var movement = input * e.moveSpeed.speed;

            var newPos = rb.position + movement * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
