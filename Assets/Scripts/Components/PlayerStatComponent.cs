using Unity;
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
    public float moveSpeed = .0f;

    public int maxHealth = 0;

    public eAttackType atkType = eAttackType.MELEE;
    public int attackDamage = 0;

    public bool isEnableFireball = false;
    public int fireballDamage = 0;
    public int fireballManaCost = 0;

    public int maxMana = 0;
    public int manaRegen = 0;
    public float manaTimeRegen = 1.0f;


}


public partial class CoreEntity
{
    public void AddPlayerStat(PlayerStatComponent stat)
    {
        var index = CoreComponentsLookup.PlayerStat;
        AddComponent(index, stat);

        if (hasMoveSpeed)
            ReplaceMoveSpeed(stat.moveSpeed);
        else 
            AddMoveSpeed(stat.moveSpeed);
        
        if (hasHellth)
            ReplaceHellth(hellth.curValue, stat.maxHealth);
        else 
            AddHellth(stat.maxHealth, stat.maxHealth);

        if (stat.isEnableFireball)
        {
            if (hasMana)
                ReplaceMana(mana.curValue, stat.maxMana);
            else
                AddMana(0, stat.maxMana);

            if (hasManaSpeed)
                ReplaceManaSpeed(stat.manaRegen, stat.manaTimeRegen);
            else
                AddManaSpeed(stat.manaRegen, stat.manaTimeRegen);
        }

        if (hasAttackDamage)
            ReplaceAttackDamage(stat.attackDamage);
        else 
            AddAttackDamage(stat.attackDamage);

        if (hasAttackType)
            ReplaceAttackType(stat.atkType);
        else
            AddAttackType(stat.atkType);



    }
}