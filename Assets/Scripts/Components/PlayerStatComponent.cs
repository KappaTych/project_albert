using Entitas;
using System;

[Serializable]
public enum eAttackType
{
    MELEE,
    SWORD
}

[Serializable]
[Core]
public class PlayerStatComponent : IComponent
{
    public int maxHealth = 0;

    public int maxMana = 0;
    public int manaRegen = 0;

    public eAttackType atkType = eAttackType.MELEE;
    public int attackDamage = 0;

    public bool isEnableFireball = false;
    public int fireballDamage = 0;
    public int fireballManaCost = 0;

    public float moveSpeed = .0f;
}


public partial class CoreEntity
{
    public void AddPlayerStat(PlayerStatComponent stat)
    {
        var index = CoreComponentsLookup.PlayerStat;
        AddComponent(index, stat);
    }
}