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
        Play("Level1");
    }

    public void Play(string audioName)
    {
        Audio a = Array.Find(audios, sound => sound.audioName == audioName);
        a.audioSource.Play();
    }
}
