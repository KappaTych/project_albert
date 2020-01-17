using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarView : MonoBehaviour, IEventListener, IHellthListener
{
    public GameObject bar;

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        if (entity == null)
            return;
        
        entity.AddHellthListener(this);
        OnHellth(entity, entity.hellth.curValue, entity.hellth.maxValue);
    }

    public void UnregisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity?.RemoveHellthListener();
    }

    public void OnHellth(CoreEntity entity, int curValue, int maxValue)
    {
        bar?.GetComponent<BarView>()?.setValue(curValue, maxValue);
    }
}
