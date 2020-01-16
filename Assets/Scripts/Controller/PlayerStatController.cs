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
        var entity = contexts.core.CreateEntity();

        gameObject.Link(entity);
        entity.AddGameObject(gameObject);

        entity.AddPlayerStat(stat);
        entity.AddDirection(startDir);
        entity.AddAttack(false);
        entity.AddMove(Vector2.zero);
        entity.isEnableMove = true;
    }

    void Start()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>()?.RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>()?.RegisterListeners(Contexts.sharedInstance, entity); 
    }

    private void OnDestroy()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>()?.UnregisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>()?.UnregisterListeners(Contexts.sharedInstance, entity);
    }
}