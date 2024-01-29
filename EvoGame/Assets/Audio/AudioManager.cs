using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to hold your audio clips

    private AudioSource[] audioSources; // Array to hold your AudioSources

    void Start()
    {
        // Initialize the array of AudioSources
        audioSources = GetComponents<AudioSource>();

        // Check if the number of AudioSources matches the number of audio clips
        if (audioSources.Length != audioClips.Length)
        {
            Debug.LogError("Number of AudioSources does not match the number of audio clips!");
            return;
        }

        // Load audio clips into AudioSources
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].clip = audioClips[i];
        }
    }

    // Example method to play a specific sound
    public void PlayEffect(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioSources.Length)
        {

            audioSources[soundIndex].Play();
        }
        else
        {
            Debug.LogError("Invalid sound index!");
        }
    }

    // Example method to stop a specific sound
    public void StopSound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioSources.Length)
        {
            if (audioSources[soundIndex].isPlaying)
            {
                audioSources[soundIndex].Stop();
            }
        }
        else
        {
            Debug.LogError("Invalid sound index!");
        }
    }

    // Example method to check if a specific sound is playing
    public bool IsSoundPlaying(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioSources.Length)
        {
            return audioSources[soundIndex].isPlaying;
        }
        else
        {
            Debug.LogError("Invalid sound index!");
            return false;
        }
    }

    public void PlayDelayedEffect(float delay,int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioSources.Length)
        {
            if (audioSources[soundIndex].isPlaying==true) 
            {
                PlayAudioWithDelay(delay, soundIndex);


            }
        }
        else
        {
            Debug.LogError("Invalid sound index!");
        }
    }

    IEnumerator PlayAudioWithDelay(float delay,int soundIndex)
    {
        yield return new WaitForSeconds(delay);

        // Play the audio clip after the specified delay
        if (soundIndex >= 0 && soundIndex < audioSources.Length)
        {

            audioSources[soundIndex].Play();
        }
        else
        {
            Debug.LogError("Invalid sound index!");
        }
    }
}