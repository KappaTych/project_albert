using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarView : MonoBehaviour, IEventListener, IHellthListener
{
    public void Start()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        if (e == null)
            return;
        RegisterListeners(Contexts.sharedInstance, e);
    }

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddHellthListener(this);
        OnHellth(entity, entity.hellth.curValue, entity.hellth.maxValue);
    }

    public void OnHellth(CoreEntity entity, float curValue, float maxValue)
    {
        GetComponentInChildren<BarView>().setValue(curValue, maxValue);
    }
}
