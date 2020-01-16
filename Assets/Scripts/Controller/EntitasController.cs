using Entitas;
using UnityEngine;

public class EntitasController : MonoBehaviour
{
    private Systems _systems;
    private Systems _fixedSystems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("UpdateSystems")
            .Add(new EnableMoveSystem(contexts))
            .Add(new CoreEventSystems(contexts))
            .Add(new DamagesSystem(contexts))
            .Add(new DebugMessageSystem(contexts))
            .Add(new DeadSystem(contexts))
            .Add(new DestroyEntitySystem(contexts));

        _fixedSystems = new Feature("FixedUpdate")
            .Add(new ClampMoveSystem(contexts))
            .Add(new DirectionSystem(contexts));

        _systems.Initialize();
        _fixedSystems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void FixedUpdate()
    {
        _fixedSystems.Execute();
        _fixedSystems.Cleanup();
    }

    void OnDestroy()
    {
        _fixedSystems.DeactivateReactiveSystems();
        _systems.DeactivateReactiveSystems();
        _fixedSystems.TearDown();
        _systems.TearDown();
        //Contexts.sharedInstance.Reset();
        _fixedSystems = null;
        _systems = null;
    }
}