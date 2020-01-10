﻿using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    public Image vlImage;
    public Image effectImage;

    private float vl;
    [SerializeField] private float maxVl;
    [SerializeField] private float speed = 0.005f;

    void Start()
    {
        vl = maxVl;
    }

    private void Update()
    {
        vlImage.fillAmount = vl / maxVl;

        if (effectImage.fillAmount > vlImage.fillAmount)
            effectImage.fillAmount -= speed;
        else
            effectImage.fillAmount = vlImage.fillAmount;
        
    }

    public void setValue(float v) { vl = v; }

    public void setValue(float v, float m) { vl = v; maxVl = m; }
}