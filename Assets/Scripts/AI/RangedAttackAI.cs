using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAI : MonoBehaviour
{
    [SerializeField] private float range = .0f;
    public Transform target;
    
    void Update()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= range)
        {
            entity.isAttack = true;
        }
    }
}
