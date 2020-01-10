//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public MoveListenerComponent moveListener { get { return (MoveListenerComponent)GetComponent(CoreComponentsLookup.MoveListener); } }
    public bool hasMoveListener { get { return HasComponent(CoreComponentsLookup.MoveListener); } }

    public void AddMoveListener(System.Collections.Generic.List<IMoveListener> newValue) {
        var index = CoreComponentsLookup.MoveListener;
        var component = (MoveListenerComponent)CreateComponent(index, typeof(MoveListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMoveListener(System.Collections.Generic.List<IMoveListener> newValue) {
        var index = CoreComponentsLookup.MoveListener;
        var component = (MoveListenerComponent)CreateComponent(index, typeof(MoveListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMoveListener() {
        RemoveComponent(CoreComponentsLookup.MoveListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CoreMatcher {

    static Entitas.IMatcher<CoreEntity> _matcherMoveListener;

    public static Entitas.IMatcher<CoreEntity> MoveListener {
        get {
            if (_matcherMoveListener == null) {
                var matcher = (Entitas.Matcher<CoreEntity>)Entitas.Matcher<CoreEntity>.AllOf(CoreComponentsLookup.MoveListener);
                matcher.componentNames = CoreComponentsLookup.componentNames;
                _matcherMoveListener = matcher;
            }

            return _matcherMoveListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public void AddMoveListener(IMoveListener value) {
        var listeners = hasMoveListener
            ? moveListener.value
            : new System.Collections.Generic.List<IMoveListener>();
        listeners.Add(value);
        ReplaceMoveListener(listeners);
    }

    public void RemoveMoveListener(IMoveListener value, bool removeComponentWhenEmpty = true) {
        var listeners = moveListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveMoveListener();
        } else {
            ReplaceMoveListener(listeners);
        }
    }
}