﻿using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
    [SerializeField] public EnemyStatComponent stat;
    [SerializeField] private int startDir = 1;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        var entity = contexts.core.CreateEntity();

        gameObject.Link(entity);
        entity.AddGameObject(gameObject);

        entity.AddEnemyStat(stat);
        entity.AddMoveSpeed(stat.moveSpeed);
        entity.AddHellth(stat.maxHealth, stat.maxHealth);
        entity.AddAttackDamage(stat.attackDamage);
        entity.AddAttack(false);
        entity.AddDirection(startDir);
        entity.AddMove(Vector2.zero);
        entity.isDisableMoveOnAttack = true;
        entity.isEnableMove = true;        
    }

    void Start()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>().RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>().RegisterListeners(Contexts.sharedInstance, entity);
    }
}
