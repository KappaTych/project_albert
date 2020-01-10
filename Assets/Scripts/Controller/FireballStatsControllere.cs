using Entitas;
using Entitas.Unity;
using UnityEngine;

public class FireballStatsControllere : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int attackDamage;
    public Vector2 move;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        contexts.SubscribeId();
        var entity = contexts.core.CreateEntity();

        gameObject.Link(entity);
        entity.AddGameObject(gameObject);

        //entity.AddPlayerStat(stat);
        entity.AddMoveSpeed(moveSpeed);
        entity.AddHellth(1, 1);
        entity.AddAttackDamage(1);

        // todo config
        entity.AddDirection(4);
        entity.AddMove(move);
        entity.isEnableMove = true;

        //var contexts = Contexts.sharedInstance;
        //contexts.SubscribeId();
        //var entity = contexts.core.CreateEntity();

        //gameObject.Link(entity);
        //entity.AddGameObject(gameObject);
        //entity.AddMoveSpeed(moveSpeed);
        //entity.AddAttackDamage(attackDamage);
        //entity.AddMove(move);
        //entity.AddDirection(4);
        //entity.isEnableMove = true;
    }

    //private void Update()
    //{
    //    var rb = gameObject.GetComponent<Rigidbody2D>();

    //    var input = Vector2.ClampMagnitude(move, 1);
    //    var movement = input * moveSpeed;

    //    var newPos = rb.position + movement * Time.fixedDeltaTime;
    //    rb.MovePosition(newPos);
    //}

    void OnDestroy()
    {
        gameObject.Unlink();
    }
}
