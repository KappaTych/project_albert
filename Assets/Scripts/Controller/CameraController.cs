﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [CanBeNull] public Transform target;
    [SerializeField] private float smoothMoveSpeed;
    private bool _istargetNull = true;

    private void LateUpdate()
    {
        if (_istargetNull)
        {        
            var trg = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
            _istargetNull = trg == null;
            if (!_istargetNull)
            {
                target = trg;
            }
            else
            {
                return;;
            }
        }

        if (transform == null)
        {
            return;
        }
        
        transform.position = Vector3.Lerp(
            transform.position, 
            new Vector3(target.position.x, target.position.y, transform.position.z), 
            Time.deltaTime * smoothMoveSpeed);
    }
}
