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
        entity.AddDirection(startDir);
        entity.AddAttack(false);
        entity.AddMove(Vector2.zero);
        entity.isEnableMove = true;
        entity.AddKillMob(0, 0);

        contexts.core.isPlayerDead = false;
    }

    void Start()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>()?.RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<ManaBarView>()?.RegisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>()?.RegisterListeners(Contexts.sharedInstance, entity); 
        gameObject.GetComponent<HealthIncreaseController>()?.RegisterListeners(Contexts.sharedInstance, entity); 

        var gc = GameObject.FindGameObjectWithTag("GameController");
        gc?.GetComponent<HealthIncreaseController>()?.RegisterListeners(Contexts.sharedInstance, entity);
    }

    private void OnDestroy()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        gameObject.GetComponent<HealthBarView>()?.UnregisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<ManaBarView>()?.UnregisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<EntityAnimationView>()?.UnregisterListeners(Contexts.sharedInstance, entity);
        gameObject.GetComponent<HealthIncreaseController>()?.UnregisterListeners(Contexts.sharedInstance, entity);

        var gc = GameObject.FindGameObjectWithTag("GameController");
        gc?.GetComponent<HealthIncreaseController>()?.UnregisterListeners(Contexts.sharedInstance, entity);
    }
}