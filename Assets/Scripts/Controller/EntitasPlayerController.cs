using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntitasPlayerController : MonoBehaviour
{
    Systems _systems;
    Systems _fixedSystems;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();

        gameObject.Link(contexts.core.CreateEntity());

        var entity = gameObject.GetEntity<CoreEntity>();
        entity.AddGameObject(gameObject);
        // todo config
        entity.AddMoveSpeed(2.0f);
        entity.AddDirection(eMovement.Right);
        entity.AddHellth(100, 100);
    }

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("PlayerSystems")
            .Add(new CoreEventSystems(contexts))
            .Add(new DebugMessageSystem(contexts));

        _fixedSystems = new Feature("PlayerFixedUpdate")
            .Add(new InputMoveSystem(contexts));

        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void FixedUpdate()
    {
        _fixedSystems.Execute();
    }

    void OnDestroy()
    {
        _systems.TearDown();
        gameObject.Unlink();
    }
}