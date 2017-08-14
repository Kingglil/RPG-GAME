using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] clips;
    public static AudioClip[] _clips;
    private AudioSource src;
    private static bool isPlaying = false;
    private static bool xd;

    public static int selectedIndex = 0;

    void Start()
    {
        src = GetComponent<AudioSource>();
        //test
        //src.Stop();
        src.enabled = false;

        xd = src == GetComponent<AudioSource>();

        _clips = clips;
    }

    void Update()
    {
        if (src.clip != _clips[selectedIndex])
            src.clip = _clips[selectedIndex];

        if (isPlaying)
        {
            src.enabled = true;
            print(src.volume);
        }
        else
            src.enabled = false;
    }

    public static void Play()
    {
        print(xd);
        isPlaying = true;
    }

    public static void Stop()
    {
        isPlaying = false;
    }
}
