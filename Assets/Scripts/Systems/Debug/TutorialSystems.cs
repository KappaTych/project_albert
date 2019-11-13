using Entitas;

public class TutorialSystems : Feature
{
    public TutorialSystems(Contexts contexts) : base("Tutorial Systems")
    {
        Add(new LogMouseClickSystem(contexts));
        Add(new DebugMessageSystem(contexts));
    }
}