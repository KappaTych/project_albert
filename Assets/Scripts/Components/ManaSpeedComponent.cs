using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Core]
public class ManaSpeedComponent : IComponent
{
    public int manaRegen;
    public float manaTimeRegen;
}