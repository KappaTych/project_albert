using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        addCollistion(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        addCollistion(collision);
    }

    void addCollistion(Collider2D collision)
    {
        var entity = owner?.GetEntity<CoreEntity>();
        var other_entity = collision?.gameObject?.GetEntity<CoreEntity>();
        if (other_entity == null)
            other_entity = collision?.transform?.parent?.gameObject?.GetEntity<CoreEntity>();
        
        if (entity == null || other_entity == null || !other_entity.hasEntityId)
        {
            return;
        }
        if (!entity.hasCollision)
            entity.AddCollision(other_entity.entityId.id);
    }
}
