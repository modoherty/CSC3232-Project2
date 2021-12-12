using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagement : MonoBehaviour
{
    // Array of sound clips
    public Audio[] audios;

    void Awake()
    {
        // Adds an audio source for each specific sound to the AudioManagement class
        foreach(Audio a in audios)
        {
            a.audioSource = gameObject.AddComponent<AudioSource>();
            a.audioSource.clip = a.audioClip;

            // Attributes for the sound - volume, pitch and whether it should loop
            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;

            /* Ensures that no sounds play immediately, to prevent them from all
               playing at the same time when the audio manager is reloaded (e.g. 
               resuming the game from the pause menu) */
            a.audioSource.playOnAwake = false;
        }

    }

    public void Start()
    {
        // Gets the currently active scene - the level the player is on
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Plays each level's specific theme
        if (currentScene == "MainMenu")
            Play("MainMenu");

        else if (currentScene == "Level1" || currentScene == "Level3")
            Play("Level1");

        else if (currentScene == "MiniGame1" || currentScene == "MiniGame2")
            Play("MiniGame");

        else if (currentScene == "Level2")
            Play("Level2");
    }

    // This method takes the name of the audio clip and will play it in the level.
    public void Play(string audioName)
    {
        // Finds the correct sound in the audios array
        Audio a = Array.Find(audios, sound => sound.audioName == audioName);
        // Plays the audio
        a.audioSource.Play();
    }

    // This method will pause the given audio.
    public void Pause(string audioName)
    {
        // Finds the correct sound in the audios array
        Audio a = Array.Find(audios, sound => sound.audioName == audioName);
        // Pauses the audio
        a.audioSource.Pause();
    }
}
