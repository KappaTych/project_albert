using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Collections.Generic;

public class DeadSystem : ReactiveSystem<CoreEntity>
{
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Hellth);
    readonly Contexts _contexts;

    public DeadSystem(Contexts contexts) : base(contexts.core) {
        _contexts = contexts;
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(_matcher);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasHellth;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isDead = e.hellth.curValue <= 0;
            e.isEnableMove = !e.isDead;
            if (e.isDead && e.hasPlayerStat)
                _contexts.core.isPlayerDead = true; 
        }
    }
}
