using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    public Image vlImage;
    public Image effectImage;

    [SerializeField] private float vl;
    [SerializeField] private float maxVl;
    [SerializeField] private float speed = 0.005f;
    private float eps = 0.001f;

    void Start()
    {
        vl = maxVl;
    }

    private void Update()
    {
        vlImage.fillAmount = vl / maxVl;

        if (effectImage.fillAmount - vlImage.fillAmount > eps)
            effectImage.fillAmount -= speed;
        else if (vlImage.fillAmount - effectImage.fillAmount > eps)
            effectImage.fillAmount += speed;
        else
            effectImage.fillAmount = vlImage.fillAmount;
        
    }

    public void setValue(float v) { vl = v; }

    public void setValue(float v, float m) { vl = v; maxVl = m; }
}
