//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreEntity {

    public PlayerStatComponent playerStat { get { return (PlayerStatComponent)GetComponent(CoreComponentsLookup.PlayerStat); } }
    public bool hasPlayerStat { get { return HasComponent(CoreComponentsLookup.PlayerStat); } }

    public void AddPlayerStat(float newMoveSpeed, int newMaxHealth, eAttackType newAtkType, int newAttackDamage, bool newIsEnableFireball, int newFireballDamage, int newFireballManaCost, float newFireballSpeed, int newMaxMana, int newManaRegen, float newManaTimeRegen) {
        var index = CoreComponentsLookup.PlayerStat;
        var component = (PlayerStatComponent)CreateComponent(index, typeof(PlayerStatComponent));
        component.moveSpeed = newMoveSpeed;
        component.maxHealth = newMaxHealth;
        component.atkType = newAtkType;
        component.attackDamage = newAttackDamage;
        component.isEnableFireball = newIsEnableFireball;
        component.fireballDamage = newFireballDamage;
        component.fireballManaCost = newFireballManaCost;
        component.fireballSpeed = newFireballSpeed;
        component.maxMana = newMaxMana;
        component.manaRegen = newManaRegen;
        component.manaTimeRegen = newManaTimeRegen;
        AddComponent(index, component);
    }

    public void ReplacePlayerStat(float newMoveSpeed, int newMaxHealth, eAttackType newAtkType, int newAttackDamage, bool newIsEnableFireball, int newFireballDamage, int newFireballManaCost, float newFireballSpeed, int newMaxMana, int newManaRegen, float newManaTimeRegen) {
        var index = CoreComponentsLookup.PlayerStat;
        var component = (PlayerStatComponent)CreateComponent(index, typeof(PlayerStatComponent));
        component.moveSpeed = newMoveSpeed;
        component.maxHealth = newMaxHealth;
        component.atkType = newAtkType;
        component.attackDamage = newAttackDamage;
        component.isEnableFireball = newIsEnableFireball;
        component.fireballDamage = newFireballDamage;
        component.fireballManaCost = newFireballManaCost;
        component.fireballSpeed = newFireballSpeed;
        component.maxMana = newMaxMana;
        component.manaRegen = newManaRegen;
        component.manaTimeRegen = newManaTimeRegen;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerStat() {
        RemoveComponent(CoreComponentsLookup.PlayerStat);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CoreMatcher {

    static Entitas.IMatcher<CoreEntity> _matcherPlayerStat;

    public static Entitas.IMatcher<CoreEntity> PlayerStat {
        get {
            if (_matcherPlayerStat == null) {
                var matcher = (Entitas.Matcher<CoreEntity>)Entitas.Matcher<CoreEntity>.AllOf(CoreComponentsLookup.PlayerStat);
                matcher.componentNames = CoreComponentsLookup.componentNames;
                _matcherPlayerStat = matcher;
            }

            return _matcherPlayerStat;
        }
    }
}
