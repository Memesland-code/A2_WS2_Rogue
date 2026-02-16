using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static int currentMusicIndex = 0;
    private static AudioSource MusicAudioSource;
    public AudioClip FightMusic;
    public AudioClip MenuMusic;

    void Start()
    {
        currentMusicIndex = 0;
        MusicAudioSource = GetComponent<AudioSource>();
        ChangeMusic();
    }


    public static void ChangeMusic()
    {
        //ChangingMusic();
    }

    public void ChangingMusic()
    {
        Debug.Log("Changing Music");

        if (currentMusicIndex == 0) //play menu music
        {
            MusicAudioSource.clip = MenuMusic;
            //MusicAudioSource.Stop();
            MusicAudioSource.Play();
            currentMusicIndex = 1;
        }

        if (currentMusicIndex == 1) // play fight music
        {
            MusicAudioSource.clip = FightMusic;
            //MusicAudioSource.Stop();
            MusicAudioSource.Play();
            currentMusicIndex = 0;
        }
    }
}

