using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Save
{
    public PlayerStatComponent Stats;
    public string LastLevel;
    public int HP;
    public int Exp;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

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

    public Save CreateSave(string lastLevel)
    {
        var save = new Save();
        var entity = GameObject.Find("Player")?.GetEntity<CoreEntity>();
        var playerStat = entity.playerStat;
        save.HP = entity.hellth.curValue;
        save.Exp = 0;
        save.Stats = playerStat;
        save.LastLevel = lastLevel;
        return save;
    }

    public void Save(string lastLevel = "")
    {
        var save = CreateSave(lastLevel);
        var bf = new BinaryFormatter();
        using (var file = File.Create("save.save"))
        {
            bf.Serialize(file, save);
        }
    }

    public Save LoadSave()
    {
        Save save;
        var bf = new BinaryFormatter();
        using (var file = File.Open("save.save", FileMode.Open))
        {
            save = (Save)bf.Deserialize(file);
        }
        return save;
    }
}