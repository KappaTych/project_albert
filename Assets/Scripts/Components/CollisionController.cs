using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var entity = owner?.GetEntity<CoreEntity>();
        var other_entity = collision.gameObject?.GetEntity<CoreEntity>();
        if (entity == null || other_entity == null || !other_entity.hasEntityId)
        {
            return;
        }
        entity.AddCollision(other_entity.entityId.id);
    }
}
