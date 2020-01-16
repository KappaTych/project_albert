using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManaRegenerateController : MonoBehaviour
{
    private IEnumerator coroutine;

    void Start()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        if (e.hasMana && e.hasManaSpeed)
        {
            coroutine = regenerateMana(e.manaSpeed.manaTimeRegen);
            StartCoroutine(coroutine);
        }
            
    }

    IEnumerator regenerateMana(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            var e = gameObject.GetEntity<CoreEntity>();
            // TODO replace
            if (e.hasMana && e.hasManaSpeed && e.mana.curValue != e.mana.maxValue &&
                !e.isAnimationManaBar)
            {
                var newMana = Mathf.Min(e.mana.maxValue, e.mana.curValue + e.manaSpeed.manaRegen);
                e.ReplaceMana(newMana);
            }
        }
    }
}
