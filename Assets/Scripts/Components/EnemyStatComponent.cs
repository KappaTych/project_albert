using Entitas;
using System;

[Serializable]
[Core]
public class EnemyStatComponent : IComponent
{
    public int maxHealth = 0;

    public int attackDamage = 0;

    public float moveSpeed = .0f;

    public int expOnDead = 0;
}


public partial class CoreEntity
{
    public void AddEnemyStat(EnemyStatComponent stat)
    {
        var index = CoreComponentsLookup.EnemyStat;
        AddComponent(index, stat);

        if (hasMoveSpeed)
            ReplaceMoveSpeed(stat.moveSpeed);
        else
            AddMoveSpeed(stat.moveSpeed);

        if (hasHellth)
            ReplaceHellth(hellth.curValue, stat.maxHealth);
        else
            AddHellth(stat.maxHealth, stat.maxHealth);

        if (hasAttackDamage)
            ReplaceAttackDamage(stat.attackDamage);
        else
            AddAttackDamage(stat.attackDamage);

        if (hasExperience)
            ReplaceExperience(stat.expOnDead);
        else
            AddExperience(stat.expOnDead);
    }
}