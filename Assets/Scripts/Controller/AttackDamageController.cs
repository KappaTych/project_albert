using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageController : MonoBehaviour
{
    public GameObject damageObj;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // attackArea - collider - child of entity
        var other = collider?.transform?.parent?.gameObject;
        var entity = damageObj?.GetEntity<CoreEntity>();
        var other_entity = other?.GetEntity<CoreEntity>();
        if (entity != null && other_entity != null && 
            entity.hasAttackDamage && entity.hasEntityId)
        {
            if (other_entity.hasDamage)
                other_entity.damage.value += entity.attackDamage.vl;
            else
                other_entity.AddDamage(entity.attackDamage.vl, entity.entityId.id);
        }
    }
}
