using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntingEnemyAI : MonoBehaviour
{
    // Enemy
    [SerializeField] private float minRange = .0f, maxRange = .0f;

    public Transform target;
    public Transform home;

    private void FixedUpdate()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= maxRange && distance >= minRange)
        {
            var dir = target.position - transform.position;
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
        LookAt(target);
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
}
