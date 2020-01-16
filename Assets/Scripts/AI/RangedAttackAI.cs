using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAI : MonoBehaviour
{
    public Transform spawnTransform; 
    public GameObject projectile;

    [SerializeField] private float range = .0f;
    public GameObject target;

    [SerializeField] private float projectileSpeed = 2.0f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target == null)
            return;

        var entity = gameObject.GetEntity<CoreEntity>();
        var distance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();
        if (distance <= range && target.activeInHierarchy)
        {
            entity?.ReplaceAttack(true);
        }
    }

    void LookAtTarget()
    {
        if (target == null)
            return;

        var r = target.transform.position - transform.position;
        var dir = MovementExtensions.GetCounterClockDirection(r);
        gameObject.GetEntity<CoreEntity>()?.ReplaceDirection(dir);
    }

    void Spawn()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        if (target == null || entity == null)
            return;

        var dir = target.transform.position - transform.position;
        
        var instance = Instantiate(projectile, spawnTransform.position, Quaternion.identity);
        var instance_entity = instance.GetEntity<CoreEntity>();
        instance_entity?.ReplaceMove(dir);
        instance_entity?.ReplaceMoveSpeed(projectileSpeed);
        instance_entity?.ReplaceAttackDamage(entity.attackDamage.vl);
    }
}
