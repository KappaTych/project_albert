using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntitasPlayerController : MonoBehaviour
{
    Systems _systems;
    Systems _fixedSystems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        
        gameObject.Link(contexts.core.CreateEntity());
        gameObject.GetEntity<CoreEntity>().AddGameObject(gameObject);
        // todo config
        gameObject.GetEntity<CoreEntity>().AddMoveSpeed(2.0f); 
        gameObject.GetEntity<CoreEntity>().AddDirection(eMovement.Right); 

        _systems = new Feature("Systems")
            .Add(new CoreEventSystems(contexts))
            .Add(new DebugMessageSystem(contexts));

        _fixedSystems = new Feature("FixedUpdate")
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