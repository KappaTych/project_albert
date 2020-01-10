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
        var dir = MovementExtensions.GetVector2(entity.direction.dir);
        
        var instance = Instantiate(projectile, spawnTransform.position, Quaternion.identity);
        instance.GetComponent<FireballStatsControllere>().move = dir;
        //instance.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
    }
}
