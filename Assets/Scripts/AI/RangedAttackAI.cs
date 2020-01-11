using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAI : MonoBehaviour
{
    public Transform spawnTransform; 
    public GameObject projectile;

    [SerializeField] private float range = .0f;
    public Transform target;

    [SerializeField] private float projectileSpeed = 2.0f;

    void Update()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        var distance = Vector3.Distance(target.position, transform.position);
        LookAtTarget();
        if (distance <= range)
        {
            entity.isAttack = true;
        }
    }

    void LookAtTarget()
    {
        var r = target.position - transform.position;
        var dir = MovementExtensions.GetCounterClockDirection(r);
        gameObject.GetEntity<CoreEntity>().ReplaceDirection(dir);
    }

    void Spawn()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        var dir = target.position - transform.position;
        
        var instance = Instantiate(projectile, spawnTransform.position, Quaternion.identity);
        var instance_entity = instance.GetEntity<CoreEntity>();
        instance_entity?.ReplaceMove(dir);
        instance_entity?.ReplaceMoveSpeed(projectileSpeed);
        instance_entity?.ReplaceAttackDamage(entity.attackDamage.vl);
    }
}
