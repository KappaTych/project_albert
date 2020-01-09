using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAI : MonoBehaviour
{
    // Enemy
    [SerializeField] private float minRange = .0f, maxRange = .0f;

    public Transform target;
    public Transform home;

    private void Update()
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
    }
}
