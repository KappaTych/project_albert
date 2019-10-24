using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform target;
	[SerializeField] private float smoothMoveSpeed;
    // Start is called before the first frame update
    private void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
		//MARKER Method 1. traditional
		//transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
			
		//MARKER Method 2. Smooth
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * smoothMoveSpeed);
    }
}
