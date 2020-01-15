﻿using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntitasEnemyController : MonoBehaviour
{
    Systems _systems;
    Systems _fixedSystems;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();

        gameObject.Link(contexts.core.CreateEntity());

        var entity = gameObject.GetEntity<CoreEntity>();
        entity.AddGameObject(gameObject);
        // todo config
        entity.AddMove(new Vector2(0, 0));
        entity.AddDirection(4);
        entity.AddMoveSpeed(1.1f);
        entity.AddHellth(100, 100);
        entity.AddDamageInfo(10);
        entity.isEnableMove = true;
    }

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("PlayerSystems")
             .Add(new NotMoveOnAttackSystem(contexts))
            .Add(new CoreEventSystems(contexts))
            .Add(new DamagesSystem(contexts));

        _fixedSystems = new Feature("PlayerFixedUpdate")
            .Add(new ClampMoveSystem(contexts))
            .Add(new DirectionSystem(contexts));

        _systems.Initialize();
        _fixedSystems.Initialize();

        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>().RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<PlayerAnimationView>().RegisterListeners(Contexts.sharedInstance, entity);
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void FixedUpdate()
    {
        _fixedSystems.Execute();
        _fixedSystems.Cleanup();
    }

    void OnDestroy()
    {
        _fixedSystems.TearDown();
        _systems.TearDown();

        gameObject.Unlink();
    }
}