using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataController : MonoBehaviour
{

    public static DataController data;

    public Save currentSave;
    private BinaryFormatter bf;

    void Awake()
    {
        print(Application.persistentDataPath);
        bf = new BinaryFormatter();
        data = this;
        DontDestroyOnLoad(gameObject);
    }    

    public void LoadData(int id)
    {
        if (File.Exists(Application.persistentDataPath + "/save" + id.ToString() + ".svd"))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/save" + id.ToString() + ".svd", FileMode.Open);
            currentSave = bf.Deserialize(file) as Save;
        }
        else
        {
            currentSave = Save.empty;
            FileStream file = new FileStream(Application.persistentDataPath + "/save" + id.ToString() + ".svd", FileMode.Create);
            bf.Serialize(file, currentSave);
        }       
    }

    public void SaveData(int id)
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/save" + id.ToString() + ".svd", FileMode.Create);
        bf.Serialize(file, currentSave);
    }

    public static Vector3Save ConvertToVector3Save(Vector3 original)
    {
        return new Vector3Save(original.x, original.y, original.z);
    }

    public static Vector3 ConvertToVector3(Vector3Save original)
    {
        return new Vector3(original.x, original.y, original.z);
    }
}

[Serializable]
public class Save
{
    public Vector3Save playerPosition { get; set; }
    public int hp { get; set; }
    public int xp { get; set; }
    public float currentTime { get; set; }
    public Item[] inventory { get; set; }
    public float hoursPlayed { get; set; }


    /// <summary>
    /// An empty(new) save.
    /// </summary>
    public static Save empty = new Save(new Vector3Save(0, 10, 0), 100, 0, 10, new Item[15], 0);

    public Save(Vector3Save playerPosition, int hp, int xp, float currentTime, Item[] inventory, float hoursPlayed)
    {
        this.playerPosition = playerPosition;
        this.hp = hp;
        this.xp = xp;
        this.currentTime = currentTime;
        this.inventory = inventory;
        this.hoursPlayed = hoursPlayed;
    }

}

[Serializable]
public struct Vector3Save
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public Vector3Save(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
