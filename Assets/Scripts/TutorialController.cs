using Entitas;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("Systems")
            .Add(new TutorialSystems(contexts));

        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
}