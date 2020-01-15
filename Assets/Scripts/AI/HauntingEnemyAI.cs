using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntingEnemyAI : MonoBehaviour, ICollisionListener
{
    // Enemy
    [SerializeField] private float minRange = .0f, maxRange = .0f;

    public GameObject target;
    public Transform home;

    
    private void Start()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        entity.AddCollisionListener(this);
    }

    private void FixedUpdate()
    {
        var entity = gameObject.GetEntity<CoreEntity>();

        if (target == null || !target.activeInHierarchy)
        {
            entity.ReplaceMove(Vector2.zero);
            return;
        }
        
        var distance = Vector3.Distance(target.transform.position, transform.position);
        if (!entity.attack.active && distance <= maxRange && distance >= minRange)
        {
            var dir = target.transform.position - transform.position;
            entity.ReplaceMove(new Vector2(dir.x, dir.y));
            return;
        }

        if (home != null && distance > maxRange && 
            Vector3.Distance(home.position, transform.position) > Mathf.Epsilon)
        {
            var dir = home.position - transform.position;
            entity.ReplaceMove(new Vector2(dir.x, dir.y));
            return;
        }
        LookAt(target.transform);
        entity.ReplaceMove(Vector2.zero);
    }

    void LookAt(Transform gm)
    {
        if (gm == null)
            return;

        var r = gm.position - transform.position;
        var dir = MovementExtensions.GetCounterClockDirection(r);
        gameObject.GetEntity<CoreEntity>().ReplaceDirection(dir);
    }


    // todo make system
    public void OnCollision(CoreEntity entity, int otherEntityId)
    {
        entity.RemoveCollision();
        var otherEntity = target.GetEntity<CoreEntity>();
        if (otherEntity == null || !otherEntity.hasEntityId || 
            otherEntity.entityId.id != otherEntityId)
        {
            return;
        }

        entity.ReplaceAttack(true);
    }
}
