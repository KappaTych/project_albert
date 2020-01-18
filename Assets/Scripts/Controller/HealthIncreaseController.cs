using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreaseController : MonoBehaviour, IEventListener, IKillMobListener
{
    public int m = 2;
    public int increaseValue = 2;

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
        if (count > 0 && (count % m == 0) && entity.hasHellth)
        {
            var n = Mathf.Min(entity.hellth.maxValue, entity.hellth.curValue + increaseValue);
            entity.ReplaceHellth(n);
        }
    }
}
