using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DebugMessageSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    readonly IGroup<GameEntity> _debugMessages;

    public DebugMessageSystem(Contexts contexts) : base(contexts.game)
    {
        _debugMessages = contexts.game.GetGroup(GameMatcher.DebugMessage);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DebugMessage);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDebugMessage;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.debugMessage.message);
        }
    }

    public void Cleanup()
    {
        foreach (var e in _debugMessages.GetEntities())
        {
            e.Destroy();
        }
    }
}