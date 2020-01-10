using Entitas;
using UnityEngine;

public class EntitasController : MonoBehaviour
{
    private Systems _systems;
    private Systems _fixedSystems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("PlayerSystems")
            .Add(new NotMoveOnAttackSystem(contexts))
            .Add(new CoreEventSystems(contexts))
            .Add(new DamagesSystem(contexts))
            .Add(new DebugMessageSystem(contexts));

        _fixedSystems = new Feature("PlayerFixedUpdate")
            .Add(new ClampMoveSystem(contexts))
            .Add(new VelocityMoveSystem(contexts))
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
        _fixedSystems.TearDown();
        _systems.TearDown();
    }
}