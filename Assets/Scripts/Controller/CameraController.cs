using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [CanBeNull] public Transform target;
    [SerializeField] private float smoothMoveSpeed;

    private void LateUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        }

        if (transform == null || target == null)
        {
            return;
        }
        
        transform.position = Vector3.Lerp(
            transform.position, 
            new Vector3(target.position.x, target.position.y, transform.position.z), 
            Time.deltaTime * smoothMoveSpeed);
    }
}
