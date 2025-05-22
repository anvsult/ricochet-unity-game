using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    // [SerializeField] private Slider musicSlider;
    // [SerializeField] private Slider sfxSlider;
    private Slider musicSlider;
    private Slider sfxSlider;

    private void Start()
    {
        musicSlider = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider")?.GetComponent<Slider>();
        musicSlider = Resources.FindObjectsOfTypeAll<Slider>().FirstOrDefault(s => s.name == "MusicSlider");
        sfxSlider = Resources.FindObjectsOfTypeAll<Slider>().FirstOrDefault(s => s.name == "SFXSlider");
        if (musicSlider == null || sfxSlider == null)
        {
            Debug.LogWarning("Sliders not found in scene.");
            return;
        }
        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((_) => SetMusicVolume());

        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener((_) => SetSFXVolume());
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }

}
