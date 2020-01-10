using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnTransform; 
    public GameObject instance;

    void Spawn()
    {
        Instantiate(instance, spawnTransform.position, Quaternion.identity);
    }
}
