using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilitiesManager : MonoBehaviour {

    public static bool slowMotion = false;
    public static bool stopTime = false;

    void Awake()
    {
        if(DataController.data == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
