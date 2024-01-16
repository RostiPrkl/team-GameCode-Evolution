using UnityEngine;
using UnityEngine.Audio;
using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.AudioController
{
    [System.Serializable]
    public class SoundData
    {
        public string ID;
        public string name;
        public string artist;
        
        [LineSeparator]
        public AudioClip audioClip;
        
        [Space]
        public AudioMixerGroup output;
        public bool mute = false;
        public bool bypassEffects = false;
        public bool bypassListenerEffects = false;
        public bool bypassReverbZones = false;
        public bool playOnAwake = true;
        public bool loop = false;
        
        [Range(0f, 256f), Space]
        public int priority = 128;
        
        [Range(0f, 1f)]
        public float volume = 1;
        
        [Range(-3f, 3f), Space(6)]
        public float pitch = 1;
        
        [Range(-1f, 1f), Space(6)]
        public float stereoPan = 0;
        
        [Range(0f, 1f)]
        public float spatialBlend = 0;
        
        [Range(0f, 1.1f)]
        public float reverbZoneMix = 1;
    }
}