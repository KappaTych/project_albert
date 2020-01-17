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
    public string defaultLevel = "SampleScene";
    

    private void Awake()
    {
        Debug.Log("save manager awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        
    }


    public Save CreateSave(string lastLevel)
    {
        Debug.Log("save level with name" + lastLevel);
        var save = new Save();
        var pl = GameObject.FindGameObjectWithTag("Player");
        var entity = pl?.GetEntity<CoreEntity>();
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

    private Save SaveAndReturn(string lastLevel = "")
    {
        var save = CreateSave(lastLevel);
        var bf = new BinaryFormatter();
        using (var file = File.Create("save.save"))
        {
            bf.Serialize(file, save);
        }
        return save;
    }

    public Save LoadSave()
    {
        Save save;
        var bf = new BinaryFormatter();
        if (File.Exists("save.save"))
        {
            using (var file = File.Open("save.save", FileMode.Open))
            {
                save = (Save)bf.Deserialize(file);
            }
        }
        else
        {
            save = SaveAndReturn(defaultLevel);
        }
        Debug.Log("load level with name" + save.LastLevel);
        return save;
    }
}