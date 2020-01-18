using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthDecreaseСontroller : MonoBehaviour
{
    private IEnumerator coroutine;

    public float activateTime = 2.0f;
    public float decreaseValue = 5;

    void Start()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        if (e.hasHellth)
        {
            coroutine = regenerateMana(activateTime);
            StartCoroutine(coroutine);
        }
            
    }

    IEnumerator regenerateMana(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            var e = gameObject.GetEntity<CoreEntity>();
            if (e.hasHellth && e.hellth.curValue >= 0) // && !e.isAnimationManaBar
            {
                var n = (int)Mathf.Max(0, e.hellth.curValue - decreaseValue);
                e.ReplaceHellth(n);
            }
        }
    }
}
