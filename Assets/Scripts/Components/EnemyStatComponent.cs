using Entitas;
using System;

[Serializable]
[Core]
public class EnemyStatComponent : IComponent
{
    public int maxHealth = 0;

    public int attackDamage = 0;

    public float moveSpeed = .0f;
}


public partial class CoreEntity
{
    public void AddEnemyStat(EnemyStatComponent stat)
    {
        var index = CoreComponentsLookup.EnemyStat;
        AddComponent(index, stat);
    }
}