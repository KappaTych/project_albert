using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour, IAnyEventListener, IAnyPlayerDeadListener
{
    private CoreEntity e;

    void Awake()
    {
        RegisterListeners(Contexts.sharedInstance);
    }

    private void OnDestroy()
    {
        UnregisterListeners(Contexts.sharedInstance);
    }
 
    public void RegisterListeners(Contexts contexts)
    {
        e = contexts.core.CreateEntity();
        e.AddAnyPlayerDeadListener(this);
    }
    public void UnregisterListeners(Contexts contexts)
    {
        e.RemoveAnyPlayerDeadListener(this);
    }

    public void OnAnyPlayerDead(CoreEntity entity)
    {
        Debug.Log("Player dead");
    }

}
