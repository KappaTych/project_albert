using Entitas;
using Entitas.Unity;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    [SerializeField] private PlayerStatComponent stat;
    [SerializeField] private int startDir = 4;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        var entity = contexts.core.CreateEntity();

        gameObject.Link(entity);
        entity.AddGameObject(gameObject);

        entity.AddPlayerStat(stat);
        entity.AddMoveSpeed(stat.moveSpeed);
        entity.AddHellth(stat.maxHealth, stat.maxHealth);
        entity.AddAttackDamage(stat.attackDamage);
        entity.AddDirection(startDir);
        entity.AddMove(Vector2.zero);
        entity.AddAttack(false);
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