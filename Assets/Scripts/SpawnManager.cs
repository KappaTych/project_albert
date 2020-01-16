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

    private void OnLevelWasLoaded(int level)
    {
        var enterence = GameObject.Find("Enterence").transform;
        var pl = Instantiate(DefaultPlayer, enterence.position, Quaternion.identity);
        var save = SaveManager.Instance?.LoadSave();
        if (save != null)
        {
            var entity = pl?.GetEntity<CoreEntity>();
            if (entity.hasPlayerStat)
            {
                entity.RemovePlayerStat();
            }
            entity.AddPlayerStat(save.Stats);
            entity.ReplaceHellth(save.HP);
        }
        pl.SetActive(true);
    }
}
