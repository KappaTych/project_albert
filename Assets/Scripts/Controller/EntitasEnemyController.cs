using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntitasEnemyController : MonoBehaviour
{
    private Systems _systems;
    private Systems _fixedSystems;
    [SerializeField] public EnemyStatComponent stat;

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

        // todo config
        entity.AddDirection(4);
        entity.AddMove(Vector2.zero);
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
