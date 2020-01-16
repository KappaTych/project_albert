using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour, IAnyEventListener, IAnyPlayerDeadListener
{
    private int entityId;

    void Start()
    {
        RegisterListeners(Contexts.sharedInstance);
    }

    private void OnDestroy()
    {
        UnregisterListeners(Contexts.sharedInstance);
    }
 
    public void RegisterListeners(Contexts contexts)
    {
        var e = contexts.core.CreateEntity();
        entityId = e.entityId.id;
        e.AddAnyPlayerDeadListener(this);
    }
    public void UnregisterListeners(Contexts contexts)
    {
        var e = contexts.core.GetEntityWithEntityId(entityId);
        if (e == null)
            return;
        e.RemoveAnyPlayerDeadListener(this);
        e.isDestroyEntity = true;
    }

    public void OnAnyPlayerDead(CoreEntity entity)
    {
        Debug.Log("Player dead");
    }

}
