using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
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
        entity.AddAttackDamage(stat.attackDamage);

        // todo config
        entity.AddDirection(1);
        entity.AddMove(Vector2.zero);
        entity.isEnableMove = true;        
    }

    void Start()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>().RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>().RegisterListeners(Contexts.sharedInstance, entity);
    }

    void OnDestroy()
    {
        gameObject.Unlink();
    }
}
