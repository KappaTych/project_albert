//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public OwnerComponent owner { get { return (OwnerComponent)GetComponent(CoreComponentsLookup.Owner); } }
    public bool hasOwner { get { return HasComponent(CoreComponentsLookup.Owner); } }

    public void AddOwner(int newOwnerId) {
        var index = CoreComponentsLookup.Owner;
        var component = (OwnerComponent)CreateComponent(index, typeof(OwnerComponent));
        component.ownerId = newOwnerId;
        AddComponent(index, component);
    }

    public void ReplaceOwner(int newOwnerId) {
        var index = CoreComponentsLookup.Owner;
        var component = (OwnerComponent)CreateComponent(index, typeof(OwnerComponent));
        component.ownerId = newOwnerId;
        ReplaceComponent(index, component);
    }

    public void RemoveOwner() {
        RemoveComponent(CoreComponentsLookup.Owner);
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

    static Entitas.IMatcher<CoreEntity> _matcherOwner;

    public static Entitas.IMatcher<CoreEntity> Owner {
        get {
            if (_matcherOwner == null) {
                var matcher = (Entitas.Matcher<CoreEntity>)Entitas.Matcher<CoreEntity>.AllOf(CoreComponentsLookup.Owner);
                matcher.componentNames = CoreComponentsLookup.componentNames;
                _matcherOwner = matcher;
            }

            return _matcherOwner;
        }
    }
}
