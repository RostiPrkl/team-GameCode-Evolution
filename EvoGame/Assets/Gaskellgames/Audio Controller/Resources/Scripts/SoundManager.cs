using System;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.AudioController
{
    public class SoundManager : MonoBehaviour
    {
        #region Variables

        //singleton
        public static SoundManager instance { get; private set; }
        
        // audio source

        [SerializeField, RequiredField] private AudioSource _music;
        private bool pauseMusic;
        private int countCheckMusic;
        [SerializeField, RequiredField] private AudioSource _soundFX;
        private bool pauseSoundFX;
        private int countCheckSoundFX;
        [SerializeField, RequiredField] private AudioSource _environment;
        private bool pauseEnvironment;
        private int countCheckEnvironment;
        [SerializeField, RequiredField] private AudioSource _menuUI;
        private bool pauseMenuUI;
        private int countCheckMenuUI;
        [Tooltip("If true, then audioSource mute will be loaded from track/sound library info")]
        [SerializeField] private bool overrideMute;

        [LineSeparator(true, false), Header("Sound Library"), Space]

        [SerializeField] private SoundData[] music;
        [SerializeField] private SoundData[] soundFX;
        [SerializeField] private SoundData[] environment;
        [SerializeField] private SoundData[] menuUI;

        [LineSeparator(true, false), Header("Playlists Library"), Space]

        [SerializeField] private List<PlaylistData> playlists;

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Game Loop

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log("Duplicate SoundController destroyed");
                Destroy(gameObject);
                return;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

#if UNITY_EDITOR

        #region Reset & OnDrawGizmos [Editor]

        private void Reset()
        {
            countCheckMusic = 0;
            countCheckSoundFX = 0;
            countCheckEnvironment = 0;
            countCheckMenuUI = 0;

            music = new SoundData[]{};
            soundFX = new SoundData[]{};
            environment = new SoundData[]{};
            menuUI = new SoundData[]{};
        }

        private void OnDrawGizmos()
        {
            SetupChannel();
        }

        #endregion

#endif

        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private void SetupChannel()
        {
            if (countCheckMusic != music.Length)
            {
                if (music.Length != 0 && countCheckMusic == 0)
                {
                    if (_music != null)
                    {
                        music[0].output = _music.outputAudioMixerGroup;
                    }
                    SetDefaultValues(music[0]);
                }

                countCheckMusic = music.Length;
            }

            if (countCheckSoundFX != soundFX.Length)
            {
                if (soundFX.Length != 0 && countCheckSoundFX == 0)
                {
                    if (_soundFX != null)
                    {
                        soundFX[0].output = _soundFX.outputAudioMixerGroup;
                    }
                    SetDefaultValues(soundFX[0]);
                }

                countCheckSoundFX = soundFX.Length;
            }

            if (countCheckEnvironment != environment.Length)
            {
                if (environment.Length != 0 && countCheckEnvironment == 0)
                {
                    if (_environment != null)
                    {
                        environment[0].output = _environment.outputAudioMixerGroup;
                    }
                    SetDefaultValues(environment[0]);
                }

                countCheckEnvironment = environment.Length;
            }

            if (countCheckMenuUI != menuUI.Length)
            {
                if (menuUI.Length != 0 && countCheckMenuUI == 0)
                {
                    if (_menuUI != null)
                    {
                        menuUI[0].output = _menuUI.outputAudioMixerGroup;
                    }
                    SetDefaultValues(menuUI[0]);
                }

                countCheckMenuUI = menuUI.Length;
            }
        }

        private void SetDefaultValues(SoundData channel)
        {
            channel.mute = false;
            channel.bypassEffects = false;
            channel.bypassListenerEffects = false;
            channel.bypassReverbZones = false;
            channel.playOnAwake = false;
            channel.loop = false;

            channel.priority = 128;
            channel.volume = 1.0f;
            channel.pitch = 1.0f;
            channel.stereoPan = 0.0f;
            channel.spatialBlend = 0.0f;
            channel.reverbZoneMix = 1.0f;
        }

        private void UpdateAudioSourceMusic(SoundData newSound)
        {
            UpdateAudioSource(_music, newSound);
        }

        private void UpdateAudioSourceSoundFX(SoundData newSound)
        {
            UpdateAudioSource(_soundFX, newSound);
        }

        private void UpdateAudioSourceEnvironment(SoundData newSound)
        {
            UpdateAudioSource(_environment, newSound);
        }

        private void UpdateAudioSourceMenuUI(SoundData newSound)
        {
            UpdateAudioSource(_menuUI, newSound);
        }

        private void UpdateAudioSource(AudioSource audioSource, SoundData newSound)
        {
            audioSource.clip = newSound.audioClip;
            audioSource.outputAudioMixerGroup = newSound.output;

            if(overrideMute)
            {
                audioSource.mute = newSound.mute;
            }
            audioSource.bypassEffects = newSound.bypassEffects;
            audioSource.bypassListenerEffects = newSound.bypassListenerEffects;
            audioSource.bypassReverbZones = newSound.bypassReverbZones;
            audioSource.playOnAwake = newSound.playOnAwake;
            audioSource.loop = newSound.loop;

            audioSource.priority = newSound.priority;
            audioSource.volume = newSound.volume;
            audioSource.pitch = newSound.pitch;
            audioSource.panStereo = newSound.stereoPan;
            audioSource.spatialBlend = newSound.spatialBlend;
            audioSource.reverbZoneMix = newSound.reverbZoneMix;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Public Functions

        public void PlayMusic(string soundID)
        {
            SoundData sound = Array.Find(music, value => value.ID == soundID);

            if (sound != null)
            {
                UpdateAudioSourceMusic(sound);
                _music.Play();
            }
        }

        public void PlaySoundFX(string soundID)
        {
            SoundData sound = Array.Find(soundFX, value => value.ID == soundID);

            if (sound != null)
            {
                UpdateAudioSourceSoundFX(sound);
                _soundFX.PlayOneShot(sound.audioClip);
            }
        }

        public void PlayEnvironment(string soundID)
        {
            SoundData sound = Array.Find(environment, value => value.ID == soundID);

            if (sound != null)
            {
                UpdateAudioSourceEnvironment(sound);
                _environment.PlayOneShot(sound.audioClip);
            }
        }

        public void PlayMenuUI(string soundID)
        {
            SoundData sound = Array.Find(menuUI, value => value.ID == soundID);

            if (sound != null)
            {
                UpdateAudioSourceMenuUI(sound);
                _menuUI.PlayOneShot(sound.audioClip);
            }
        }

        public void PauseAllSounds()
        {
            PauseMusic();
            PauseSoundFX();
            PauseEnvironment();
            PauseMenuUI();
        }

        public void PauseMusic()
        {
            if(!pauseMusic)
            {
                _music.Pause();
                pauseMusic = true;
            }
            else
            {
                _music.UnPause();
                pauseMusic = false;
            }
        }

        public void PauseSoundFX()
        {
            if (!pauseSoundFX)
            {
                _soundFX.Pause();
                pauseSoundFX = true;
            }
            else
            {
                _soundFX.UnPause();
                pauseSoundFX = false;
            }
        }

        public void PauseEnvironment()
        {
            if(!pauseEnvironment)
            {
                _environment.Pause();
                pauseEnvironment = true;
            }
            else
            {
                _environment.UnPause();
                pauseEnvironment = false;
            }
        }

        public void PauseMenuUI()
        {
            if(!pauseMenuUI)
            {
                _menuUI.Pause();
                pauseMenuUI = true;
            }
            else
            {
                _menuUI.UnPause();
                pauseMenuUI = false;
            }
        }

        public void StopAllSounds()
        {
            StopMusic();
            StopSoundFX();
            StopEnvironment();
            StopMenuUI();
        }

        public void StopMusic() { _music.Stop(); }

        public void StopSoundFX() { _soundFX.Stop(); }

        public void StopEnvironment() { _environment.Stop(); }

        public void StopMenuUI() { _menuUI.Stop(); }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Getter / Setter

        public AudioSource AudioSourceMusic
        {
            get { return _music; }
            set { _music = value; }
        }

        public AudioSource AudioSourceSoundFX
        {
            get { return _soundFX; }
            set { _soundFX = value; }
        }

        public AudioSource AudioSourceEnvironment
        {
            get { return _environment; }
            set { _environment = value; }
        }

        public AudioSource AudioSourceMenuUI
        {
            get { return _menuUI; }
            set { _menuUI = value; }
        }
        
        public SoundData GetSoundFromID(String soundID)
        {
            SoundData tempSoundData = GetSoundFromMusicID(soundID);
            if (tempSoundData != null)
            {
                return tempSoundData;
            }
            
            tempSoundData = GetSoundFromSoundFXID(soundID);
            if (tempSoundData != null)
            {
                return tempSoundData;
            }
            
            tempSoundData = GetSoundFromEnvironmentID(soundID);
            if (tempSoundData != null)
            {
                return tempSoundData;
            }
            
            tempSoundData = GetSoundFromMenuUIID(soundID);
            if (tempSoundData != null)
            {
                return tempSoundData;
            }

            return null;
        }
        
        public SoundData GetSoundFromMusicID(string soundID)
        {
            SoundData sound = Array.Find(music, value => value.ID == soundID);

            if (sound != null)
            {
                return sound;
            }
            else
            {
                return null;
            }
        }
        
        public SoundData GetSoundFromSoundFXID(string soundID)
        {
            SoundData sound = Array.Find(soundFX, value => value.ID == soundID);

            if (sound != null)
            {
                return sound;
            }
            else
            {
                return null;
            }
        }
        
        public SoundData GetSoundFromEnvironmentID(string soundID)
        {
            SoundData sound = Array.Find(environment, value => value.ID == soundID);

            if (sound != null)
            {
                return sound;
            }
            else
            {
                return null;
            }
        }
        
        public SoundData GetSoundFromMenuUIID(string soundID)
        {
            SoundData sound = Array.Find(menuUI, value => value.ID == soundID);

            if (sound != null)
            {
                return sound;
            }
            else
            {
                return null;
            }
        }
        
        public PlaylistData GetPlaylistData(string playlistID)
        {
            PlaylistData playlist = null;

            for (int i = 0; i < playlists.Count; i++)
            {
                if (playlists[i].name == playlistID)
                {
                    playlist = playlists[i];
                }
            }

            return playlist;
        }

        public string GetSoundNameFromID(string soundID)
        {
            SoundData sound = Array.Find(music, value => value.ID == soundID);

            if (sound != null)
            {
                return sound.name;
            }
            else
            {
                return null;
            }
        }

        public string GetSoundArtistFromID(string soundID)
        {
            SoundData sound = Array.Find(music, value => value.ID == soundID);

            if (sound != null)
            {
                return sound.artist;
            }
            else
            {
                return null;
            }
        }

        #endregion

    } //class end
}