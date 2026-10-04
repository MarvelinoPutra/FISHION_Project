using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISliderSound : MonoBehaviour
{
    public enum SliderType { Music, SFX }
    public SliderType sliderType; // pilih di Inspector: Music atau SFX

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        // Set posisi slider sesuai volume yang udah tersimpan
        float savedVolume = PlayerPrefs.GetFloat(
            sliderType == SliderType.Music ? "MusicVolume" : "SFXVolume",
            1f
        );
        slider.value = VolumeToSlider(savedVolume);
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(SliderToVolume(sliderValue));
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetSFXVolume(SliderToVolume(sliderValue));
    }

    float SliderToVolume(float sliderValue)
    {
        if (sliderValue <= 0.0001f) return 0f;
        float dB = Mathf.Lerp(-40f, 0f, sliderValue);
        return Mathf.Pow(10f, dB / 20f);
    }

    float VolumeToSlider(float volume) // ⬅️ TAMBAHAN: kebalikan dari SliderToVolume
    {
        if (volume <= 0.0001f) return 0f;
        float dB = 20f * Mathf.Log10(volume);
        return Mathf.InverseLerp(-40f, 0f, dB);
    }
}