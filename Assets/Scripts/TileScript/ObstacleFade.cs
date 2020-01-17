using UnityEngine;

public class ObstacleFade : MonoBehaviour
{
    private SpriteRenderer _mySpriteRenderer;
    private Color _defaultColor;
    private Color _fadedColor;
    // Start is called before the first frame update
    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _mySpriteRenderer.color;
        _fadedColor = _defaultColor;
        _fadedColor.a = 0.7f;
    }

    private void Fade(bool isFade)
    {
        _mySpriteRenderer.color = isFade ? _fadedColor : _defaultColor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Fade(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Fade(false);
        }
    }
}
