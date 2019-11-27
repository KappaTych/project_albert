using Entitas;
using Entitas.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        gameObject.Link(contexts.core.CreateEntity());

        _systems = new Feature("Systems")
            .Add(new DebugMessageSystem(contexts));

        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        _systems.TearDown();
        gameObject.Unlink();
    }
}