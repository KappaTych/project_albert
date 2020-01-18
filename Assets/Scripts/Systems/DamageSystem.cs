using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class DamagesSystem : ReactiveSystem<CoreEntity>, ICleanupSystem
{
    readonly Contexts _contexts;
    readonly IGroup<CoreEntity> _damageGroup;
    readonly IMatcher<CoreEntity> _matcher = CoreMatcher.AllOf(CoreMatcher.Damage);

    public DamagesSystem(Contexts contexts) : base(contexts.core)
    {
        _contexts = contexts; 
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
            var other_entity = _contexts.core.GetEntityWithEntityId(e.damage.damageOnwerId);
            if (e.hellth.curValue <= 0 && other_entity != null && 
                other_entity.hasKillMob && e.hasExperience)
            {
                other_entity.killMob.count += 1;
                other_entity.killMob.exp += e.experience.exp;
                other_entity.ReplaceKillMob(other_entity.killMob.count, other_entity.killMob.exp);
            }

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
