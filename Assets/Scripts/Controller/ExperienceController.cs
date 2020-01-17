using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour, IEventListener, IKillMobListener
{
    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddKillMobListener(this);
    }

    public void UnregisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.RemoveKillMobListener(this);
    }

    public void OnKillMob(CoreEntity entity, int count, int exp)
    {
        Debug.Log("Kill enemy, count: " + count + " exp: " + exp);
    }
}
