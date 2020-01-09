using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image vlImage;
    public Image effectImage;

    [HideInInspector] public float vl;
    public float maxVl;
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

}
