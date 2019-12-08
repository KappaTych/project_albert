using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionView : MonoBehaviour, IEventListener, IDirectionListener
{
    public void Start()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        if (e == null)
            return;
        RegisterListeners(Contexts.sharedInstance, e);
    }

    // Function to call after adding this View to a CoreEntity
    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddDirectionListener(this);
    }

    public void OnDirection(CoreEntity entity, eMovement dir)
    {
        var sp = gameObject.GetComponent<SpriteRenderer>();
        if (sp == null)
            return;

        if (dir == eMovement.Left || dir == eMovement.Right)
            sp.flipX = dir == eMovement.Left;
    }
}
