using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private Color _defaultColor;

    private Color _transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        var gc = GameObject.FindGameObjectWithTag("GameController");
        gc?.GetComponent<SpawnManager>()?.SpawnPlayer();
        _defaultColor = GetComponent<SpriteRenderer>().color;
        _transparentColor = _defaultColor;
        _transparentColor.a = 0;
        GetComponent<SpriteRenderer>().color = _transparentColor;
    }

    // Update is called once per frame
    void Update()
    {
    }
}