using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpeedTutorMainMenuSystem

{
    public class MenuController : MonoBehaviour
    {
        public AudioManager menuSounds;
        private float _volumeLevel;
        private int _qualityLevel;
        private bool _isFullScreen;
        private float _brightnessLevel;
        public int mainControllerSen = 4;

        [Header("Default Menu Values")]
        [SerializeField] private float defaultBrightness = 1;
        [SerializeField] private float defaultVolume = 1f;

        [Header("Confirmation Object")]
        [SerializeField] private GameObject confirmationPrompt = null;
        //[SerializeField] private GameObject noSavedGameDialog = null;


        [Header("Brightness Setting")]
        [SerializeField] private Brightness brightnessEffect = null;
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private Text brightnessText = null;

        [Header("Volume Setting")]
        [SerializeField] private Text volumeText = null;
        [SerializeField] private Slider volumeSlider = null;


        [Header("Levels To Load")]
        public string _newGameButtonLevel;
        private string levelToLoad;

        [SerializeField] private Toggle fullScreenToggle;

        // BASIC CONTROLS
        public void ClickNewGameDialog()
        {
            SceneManager.LoadScene(_newGameButtonLevel);
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        // Volume Settings
        public void SetVolume(float volume)
        {
            _volumeLevel = volume;
            volumeText.text = volume.ToString("0.0");
        }

        public void VolumeApply()
        {
            PlayerPrefs.SetFloat("masterVolume", _volumeLevel);
            AudioListener.volume = _volumeLevel;
            StartCoroutine(ConfirmationBox());
        }

        // Graphics
        public void SetBrightness(float brightness)
        {
            _brightnessLevel = brightness;
            brightnessText.text = brightness.ToString("0.0");
        }

        public void SetFullScreen(bool isFullscreen)
        {
            _isFullScreen = isFullscreen;
        }

        public void GraphicsApply()
        {
            PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
            brightnessEffect.brightness = _brightnessLevel;

            PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
            Screen.fullScreen = _isFullScreen;

            StartCoroutine(ConfirmationBox());
        }

        // Gameplay Settings
        public void ResetButton(string GraphicsMenu)
        {
            if (GraphicsMenu == "Graphics")
            {
                brightnessSlider.value = defaultBrightness;
                brightnessText.text = defaultBrightness.ToString("0.0");

                fullScreenToggle.isOn = false;
                Screen.fullScreen = false;
            }

            if (GraphicsMenu == "Audio")
            {
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeText.text = defaultVolume.ToString("0.0");
                VolumeApply();
            }
        }

        public IEnumerator ConfirmationBox()
        {
            confirmationPrompt.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationPrompt.SetActive(false);
        }

        public void Start()
        {
            menuSounds = FindObjectOfType<AudioManager>();
            //menuSounds.PlayEffect(29);
        }
    }
}
