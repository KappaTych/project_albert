//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class CoreEventSystems : Feature {

    public CoreEventSystems(Contexts contexts) {
        Add(new DirectionEventSystem(contexts)); // priority: 0
        Add(new HellthEventSystem(contexts)); // priority: 0
        Add(new MoveEventSystem(contexts)); // priority: 0
    }
}
