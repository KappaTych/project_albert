using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideObstaclesTiles : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        var defaultColor = _tilemap.color;
        defaultColor.a = 0.0f;
        _tilemap.color = defaultColor;
    }
}