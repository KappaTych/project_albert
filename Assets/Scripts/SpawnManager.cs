using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; set; }
    public GameObject DefaultPlayer;

    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPlayer()
    {
        Transform enterence = GameObject.FindGameObjectWithTag("Entrance").transform;
        Instantiate(DefaultPlayer, enterence.position, Quaternion.identity);
        DefaultPlayer.SetActive(true);
    }
}
