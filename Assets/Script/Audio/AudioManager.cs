using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Gameplay SFX (Isi dengan Drag & Drop)")]
    public AudioClip dropSFX;
    public AudioClip mergeSFX;
    public AudioClip comboSFX;
    public AudioClip ggSFX;
    public AudioClip gameOverSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Abadi melintasi scene

            // Panggil memori saat game baru dibuka
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ==========================================
    // SISTEM VOLUME (DISAMBUNGKAN KE SLIDER)
    // ==========================================
    public void SetMusicVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Simpan ke memori
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);   // Simpan ke memori
    }

    private void LoadVolumeSettings()
    {
        // Ambil angka dari memori. Kalau pemain baru pertama kali main, set ke 1 (100%)
        bgmSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    // ==========================================
    // FUNGSI GANTI LAGU & SFX (Drag & Drop)
    // ==========================================
    public void ChangeBGM(AudioClip newMusic)
    {
        if (bgmSource.clip == newMusic) return;
        bgmSource.Stop();
        bgmSource.clip = newMusic;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayDropSound() => PlaySFX(dropSFX);
    public void PlayMergeSound() => PlaySFX(mergeSFX);
    public void PlayComboSound() => PlaySFX(comboSFX);
    public void PlayGGSound() => PlaySFX(ggSFX);
    public void PlayGameOverSound() => PlaySFX(gameOverSFX);
}