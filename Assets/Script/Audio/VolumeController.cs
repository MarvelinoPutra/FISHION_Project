using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Drag Slider dari Canvas ke Sini")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // 1. Sesuaikan posisi slider agar sama dengan memori yang tersimpan
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        }
    }

    // 2. Fungsi ini akan otomatis terpanggil setiap kali kamu menggeser slider
    private void UpdateMusicVolume(float value)
    {
        if (SoundManager.Instance != null) SoundManager.Instance.SetMusicVolume(value);
    }

    private void UpdateSFXVolume(float value)
    {
        if (SoundManager.Instance != null) SoundManager.Instance.SetSFXVolume(value);
    }
}