using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagement : MonoBehaviour
{
    public Audio[] audios;

    void Awake()
    {
        foreach(Audio a in audios)
        {
            a.audioSource = gameObject.AddComponent<AudioSource>();
            a.audioSource.clip = a.audioClip;

            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;
        }

    }

    void Start()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentScene == "MainMenu")
            Play("MainMenu");

        else if (currentScene == "Level1")
            Play("Level1");

        else if (currentScene == "MiniGame1")
            Play("MiniGame");

        else if (currentScene == "Level2")
            Play("Level2");
    }

    public void Play(string audioName)
    {
        Audio a = Array.Find(audios, sound => sound.audioName == audioName);
        a.audioSource.Play();
    }
}
