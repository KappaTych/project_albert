using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideColliderColor : MonoBehaviour
{

	private TilemapRenderer tilemapRenderer;

	private void Awake()
	{
		tilemapRenderer = GetComponent<TilemapRenderer>();
	}
	// Start is called before the first frame update
	private void Start()
    {
		tilemapRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
