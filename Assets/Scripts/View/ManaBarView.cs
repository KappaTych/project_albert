using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarView : MonoBehaviour, IEventListener, IManaListener
{
    public GameObject bar;

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        if (entity == null)
            return;

        entity.AddManaListener(this);
        if (entity.hasMana && entity.hasManaSpeed)
            OnMana(entity, entity.mana.curValue, entity.mana.maxValue);
    }

    public void UnregisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity?.RemoveManaListener(this);
    }

    public void OnMana(CoreEntity entity, int curValue, int maxValue)
    {
        bar?.GetComponent<BarView>()?.setValue(curValue, maxValue);
    }

    // TODO replace
    private void FixedUpdate()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        var barView = bar?.GetComponent<BarView>();
        if (barView == null || e == null)
            return;

        bar?.SetActive(e.hasManaSpeed && e.hasMana);
        e.isAnimationManaBar = barView.isAnimationActivate(); 
    }
}
