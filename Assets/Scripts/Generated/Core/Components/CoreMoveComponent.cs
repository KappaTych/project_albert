//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public MoveComponent move { get { return (MoveComponent)GetComponent(CoreComponentsLookup.Move); } }
    public bool hasMove { get { return HasComponent(CoreComponentsLookup.Move); } }

    public void AddMove(UnityEngine.Vector2 newMovement) {
        var index = CoreComponentsLookup.Move;
        var component = (MoveComponent)CreateComponent(index, typeof(MoveComponent));
        component.movement = newMovement;
        AddComponent(index, component);
    }

    public void ReplaceMove(UnityEngine.Vector2 newMovement) {
        var index = CoreComponentsLookup.Move;
        var component = (MoveComponent)CreateComponent(index, typeof(MoveComponent));
        component.movement = newMovement;
        ReplaceComponent(index, component);
    }

    public void RemoveMove() {
        RemoveComponent(CoreComponentsLookup.Move);
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

    static Entitas.IMatcher<CoreEntity> _matcherMove;

    public static Entitas.IMatcher<CoreEntity> Move {
        get {
            if (_matcherMove == null) {
                var matcher = (Entitas.Matcher<CoreEntity>)Entitas.Matcher<CoreEntity>.AllOf(CoreComponentsLookup.Move);
                matcher.componentNames = CoreComponentsLookup.componentNames;
                _matcherMove = matcher;
            }

            return _matcherMove;
        }
    }
}
