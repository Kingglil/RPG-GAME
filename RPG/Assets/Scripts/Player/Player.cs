using UnityEngine;

public class Player : MonoBehaviour 
{
    public static Vector3 position;
    public static int health;
    public static int maxHealth = 100;
    public static int xp;

    public static float moveSpeed = 5f;
    public static float jumpForce = 35f;
    public static float speedBoost = 3f;

    void Awake()
    {
        position = DataController.ConvertToVector3(DataController.data.currentSave.playerPosition);
        health = DataController.data.currentSave.hp;
        xp = DataController.data.currentSave.xp;
    }

    public static void Save()
    {
        DataController.data.currentSave = new Save(DataController.ConvertToVector3Save(position), health, xp, DataController.data.currentSave.currentTime, new Item[15], DataController.data.currentSave.hoursPlayed + Time.timeSinceLevelLoad / 3600);
        DataController.data.SaveData(0);
    }

    public static void Die()
    {

    }
}
