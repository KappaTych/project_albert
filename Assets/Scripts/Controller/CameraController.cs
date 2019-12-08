using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform target;
	[SerializeField] private float smoothMoveSpeed;
    private void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
		//MARKER Method 1. traditional
		//transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
			
		//MARKER Method 2. Smooth
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * smoothMoveSpeed);
    }
}
