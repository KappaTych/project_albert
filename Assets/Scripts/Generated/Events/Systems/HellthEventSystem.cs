//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class HellthEventSystem : Entitas.ReactiveSystem<CoreEntity> {

    readonly System.Collections.Generic.List<IHellthListener> _listenerBuffer;

    public HellthEventSystem(Contexts contexts) : base(contexts.core) {
        _listenerBuffer = new System.Collections.Generic.List<IHellthListener>();
    }

    protected override Entitas.ICollector<CoreEntity> GetTrigger(Entitas.IContext<CoreEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(CoreMatcher.Hellth)
        );
    }

    protected override bool Filter(CoreEntity entity) {
        return entity.hasHellth && entity.hasHellthListener;
    }

    protected override void Execute(System.Collections.Generic.List<CoreEntity> entities) {
        foreach (var e in entities) {
            var component = e.hellth;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.hellthListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnHellth(e, component.curValue, component.maxValue);
            }
        }
    }
}