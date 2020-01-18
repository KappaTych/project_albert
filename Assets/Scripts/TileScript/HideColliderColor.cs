using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideColliderColor : MonoBehaviour
{

	private TilemapRenderer _tilemapRenderer;

	private void Awake()
	{
		_tilemapRenderer = GetComponent<TilemapRenderer>();
	}

    private void Start()
    {
		_tilemapRenderer.enabled = false;
    }

    void Update()
    {
        
    }
}
