using Entitas;

public class DestroyEntitySystem : ICleanupSystem
{

    readonly IGroup<CoreEntity> _group;

    public DestroyEntitySystem(Contexts contexts)
    {
        _group = contexts.core.GetGroup(CoreMatcher.DestroyEntity);
    }

    public void Cleanup()
    {
        foreach (var e in _group.GetEntities())
        {
            e.Destroy();
        }
    }
}
