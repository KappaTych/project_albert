using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public float fillSpeed = 0.5f;
    
    private Transform bar;
    private float targetScale = 1;
    void Start()
    {
        bar = transform.Find("Fill Area");
    }

    private void Update()
    {
        if (bar.localScale.x < targetScale)
            return;
        SetScale(bar.localScale.x - fillSpeed * Time.deltaTime);
    }

    public void SetProggress(float s)
    {
        targetScale = bar.localScale.x - s;
    }

    private void SetScale(float s)
    {
        bar.localScale = new Vector3(s, 1);
    }
}
