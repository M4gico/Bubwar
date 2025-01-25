using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider effectVolumeSlider;
    

    [Header("Resolution")]
    [SerializeField]
    private TMPro.TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    [Header("Fullscreen")]
    [SerializeField] 
    private Toggle fullscreenToggle;
    
    public void Start()
    {
        // Volume settings
        musicVolumeSlider.maxValue = 1;
        musicVolumeSlider.minValue = 0;
        effectVolumeSlider.maxValue = 1;
        effectVolumeSlider.minValue = 0;
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectVolumeSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.5f);
        SetMusicVolume(musicVolumeSlider.value);
        SetEffectVolume(effectVolumeSlider.value);

        // Resolution settings 
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate) 
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        filteredResolutions.Sort((a, b) => {
            if (a.width != b.width)
                return b.width.CompareTo(a.width);
            else
                return b.height.CompareTo(a.height);
        });

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString("0.##") + " Hz"; // Ondalık basamak sınırlandı
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height && (float)filteredResolutions[i].refreshRateRatio.value == currentRefreshRate) // double'dan float'a dönüştürüldü
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex = 0;
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);
    }

    // Resolution settings
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    // Fullscreen settings
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Volume settings
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        FMODUnity.RuntimeManager.GetVCA("vca:/MusicVCA").setVolume(volume);
    }

    public void SetEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("EffectVolume", volume);
        FMODUnity.RuntimeManager.GetVCA("vca:/EffectVCA").setVolume(volume);

    }
    
}
