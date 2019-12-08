using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DebugMessageSystem : ReactiveSystem<CoreEntity>, ICleanupSystem
{
    readonly IGroup<CoreEntity> _debugMessages;

    public DebugMessageSystem(Contexts contexts) : base(contexts.core)
    {
        _debugMessages = contexts.core.GetGroup(CoreMatcher.DebugMessage);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(CoreMatcher.DebugMessage);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasDebugMessage;
    }

    protected override void Execute(List<CoreEntity> entities)
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