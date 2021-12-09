using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Allows the sound names/volume to be adjusted in the editor
[System.Serializable]
public class Audio
{
    // Name of the audio clip
    public string audioName;
    // The audio clip used to play the sound in the game
    public AudioClip audioClip;

    // Slider for audio volume - between 0 and 1
    [Range(0f, 1f)]
    public float volume;
    // Slider for audio pitch - between 0 and 2
    [Range(0f, 2f)]
    public float pitch;
    // Boolean to determine whether the sound will loop in the game
    public bool loop;

    // Keep the audio source hidden in inspector mode, so it cannot be overwritten
    [HideInInspector]
    public AudioSource audioSource;

}
