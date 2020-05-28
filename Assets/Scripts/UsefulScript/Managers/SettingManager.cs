using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    private static SettingManager _i;
    public static SettingManager i
    {
        get
        {
            if (_i == null)
            {
                _i = FindObjectOfType<SettingManager>();
            }
            return _i;
        }
    }

    public bool canInit;
    public bool isCamera2D;
    Resolution[] allResolutions;
    List<Resolution> resolutionFor16_9 = new List<Resolution>();
    [SerializeField]
    private Dropdown resolutionDisplay;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundEffectSlider;
    [SerializeField]
    Toggle fullScreenToggle;
    public int currentResolutionIndex;

    float musicVolume;
    float soundEffectVolume;
    
    [SerializeField]
    private AudioMixer Music;
    [SerializeField]
    private AudioMixer SoundEffect;

    [SerializeField]
    private AudioMixerGroup musicOutPutGroup;
    [SerializeField]
    private AudioMixerGroup soundEffectOutPutGroup;

    //This is for 2D game Camera
    //CameraRatio ratio;


    private void Awake()
    {
        if (canInit)
        {
            allResolutions = Screen.resolutions;
            resolutionDisplay.ClearOptions();

            List<string> optionsText = new List<string>();

            for (int i = 0; i < allResolutions.Length; i++)
            {
                string resolutions = allResolutions[i].width + "x" + allResolutions[i].height;
                optionsText.Add(resolutions);
                if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            soundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");
            fullScreenToggle.isOn = Screen.fullScreen;


            resolutionDisplay.AddOptions(optionsText);
            resolutionDisplay.value = currentResolutionIndex;
            resolutionDisplay.RefreshShownValue();
        }
    }

    private void Start()
    {
        if (canInit)
        {
            if (isCamera2D)
            {
                //ratio = FindObjectOfType<CameraRatio>();
                resolutionDisplay.onValueChanged.AddListener(delegate
                {
                    //ratio.FitRatio();
                    SetResolution(resolutionDisplay);

                });
            }
            else {
                resolutionDisplay.onValueChanged.AddListener(delegate
                {
                    SetResolution(resolutionDisplay);
                });
            }

            Music.SetFloat("Volume", PlayerPrefs.GetFloat("MusicVolume"));
            SoundEffect.SetFloat("Volume", PlayerPrefs.GetFloat("SoundEffectVolume"));

            musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(musicSlider); });
            soundEffectSlider.onValueChanged.AddListener(delegate { SetSoundEffectVolume(soundEffectSlider); });
            fullScreenToggle.onValueChanged.AddListener(delegate
            {
                SetFullScreen(fullScreenToggle);

            });
        }
    }


    public void SetFullScreen(Toggle isFullScreen)
    {
        Screen.fullScreen = isFullScreen.isOn;
    }



    public void SetResolution(Dropdown rsIndex)
    {
        Resolution rs = allResolutions[rsIndex.value];
        Screen.SetResolution(rs.width, rs.height, Screen.fullScreen);
    }


    public void SetMusicVolume(Slider slider)
    {
        musicVolume = slider.value;
        Music.SetFloat("Volume", musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSoundEffectVolume(Slider slider)
    {
        soundEffectVolume = slider.value;
        SoundEffect.SetFloat("Volume", soundEffectVolume);
        PlayerPrefs.SetFloat("SoundEffectVolume", soundEffectVolume);
    }

    public AudioMixerGroup GetMusicAudioMixer()
    {
        return musicOutPutGroup;
    }

    public AudioMixerGroup GetSoundEffectAudioMixer()
    {
        return soundEffectOutPutGroup;
    }
}
