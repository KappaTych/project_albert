using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class DamagesSystem : ReactiveSystem<CoreEntity>, ICleanupSystem
{
    readonly IGroup<CoreEntity> _damageGroup;
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Damage);

    public DamagesSystem(Contexts contexts) : base(contexts.core)
    {
        _damageGroup = contexts.core.GetGroup(_matcher);
    }

    protected override ICollector<CoreEntity> GetTrigger(IContext<CoreEntity> context)
    {
        return context.CreateCollector(_matcher);
    }

    protected override bool Filter(CoreEntity entity)
    {
        return entity.hasDamage && entity.hasHellth;
    }

    protected override void Execute(List<CoreEntity> entities)
    {
        foreach (var e in entities)
        {
            e.ReplaceHellth(e.hellth.curValue - e.damage.value);

        }
    }

    public void Cleanup()
    {
        foreach (var e in _damageGroup.GetEntities())
        {
            e.RemoveDamage();
        }
    }
}
