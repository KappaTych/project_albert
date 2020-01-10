using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageController : MonoBehaviour
{
    public GameObject damageObj;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var entity = damageObj?.GetEntity<CoreEntity>();
        var other_entity = other.gameObject.GetEntity<CoreEntity>();
        if (entity != null && other_entity != null && entity.hasPlayerStat)
        {
            other_entity.AddDamage(entity.playerStat.attackDamage);
        }
    }
}
