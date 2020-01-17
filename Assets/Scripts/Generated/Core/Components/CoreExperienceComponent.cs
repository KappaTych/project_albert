//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public ExperienceComponent experience { get { return (ExperienceComponent)GetComponent(CoreComponentsLookup.Experience); } }
    public bool hasExperience { get { return HasComponent(CoreComponentsLookup.Experience); } }

    public void AddExperience(int newExp) {
        var index = CoreComponentsLookup.Experience;
        var component = (ExperienceComponent)CreateComponent(index, typeof(ExperienceComponent));
        component.exp = newExp;
        AddComponent(index, component);
    }

    public void ReplaceExperience(int newExp) {
        var index = CoreComponentsLookup.Experience;
        var component = (ExperienceComponent)CreateComponent(index, typeof(ExperienceComponent));
        component.exp = newExp;
        ReplaceComponent(index, component);
    }

    public void RemoveExperience() {
        RemoveComponent(CoreComponentsLookup.Experience);
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

    static Entitas.IMatcher<CoreEntity> _matcherExperience;

    public static Entitas.IMatcher<CoreEntity> Experience {
        get {
            if (_matcherExperience == null) {
                var matcher = (Entitas.Matcher<CoreEntity>)Entitas.Matcher<CoreEntity>.AllOf(CoreComponentsLookup.Experience);
                matcher.componentNames = CoreComponentsLookup.componentNames;
                _matcherExperience = matcher;
            }

            return _matcherExperience;
        }
    }
}