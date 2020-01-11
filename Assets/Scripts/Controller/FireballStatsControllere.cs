using Entitas;
using Entitas.Unity;
using UnityEngine;

public class FireballStatsControllere : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private Vector2 move;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        var entity = contexts.core.CreateEntity();

        gameObject.Link(entity);
        entity.AddGameObject(gameObject);
        entity.AddMove(move);
        entity.isEnableMove = true;
        entity.AddMoveSpeed(moveSpeed);
        entity.AddAttackDamage(attackDamage);
    }

    void OnDestroy()
    {
        gameObject.Unlink();
    }
}
