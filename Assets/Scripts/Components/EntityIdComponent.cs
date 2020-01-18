using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core, Input, Bullets]
public class EntityIdComponent : IComponent
{
    [PrimaryEntityIndex]
    public int id;
}
