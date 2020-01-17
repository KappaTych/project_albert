using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnTransform;
    
    void FireballSpawn()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        var dir = MovementExtensions.GetVector2(entity.direction.dir);

        var instance = Instantiate(projectile, spawnTransform.position, Quaternion.identity);
        var instance_entity = instance.GetEntity<CoreEntity>();
        instance_entity?.ReplaceMove(dir);
        instance_entity?.ReplaceMoveSpeed(entity.playerStat.fireballSpeed);
        instance_entity?.ReplaceAttackDamage(entity.playerStat.fireballDamage);
        instance_entity?.AddOwner(entity.entityId.id);
    }
}
