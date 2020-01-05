using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarView : MonoBehaviour, IEventListener, IHellthListener
{
    public GameObject bar;

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddHellthListener(this);
        OnHellth(entity, entity.hellth.curValue, entity.hellth.maxValue);
    }

    public void OnHellth(CoreEntity entity, float curValue, float maxValue)
    {
        bar.GetComponent<BarView>().setValue(curValue, maxValue);
    }
}
