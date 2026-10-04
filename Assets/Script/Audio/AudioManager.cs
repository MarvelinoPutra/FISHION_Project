using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources (Tidak perlu diisi, otomatis!)")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM (Lagu)")]
    public AudioClip bgmMainMenu;
    public AudioClip bgmGameplay;
    [Tooltip("Pastikan nama scene ini sama persis dengan nama scene menu kamu")]
    public string mainMenuSceneName = "MainMenu";

    [Header("UI & Mekanik SFX")]
    public AudioClip clickSFX;
    public AudioClip mergeSFX;
    public AudioClip gameOverSFX;

    [Header("Combo SFX (0=Jago!, 1=Keren!, 2=Mantap!)")]
    public AudioClip[] comboSFX;

    [Header("Ultra Combo SFX (Level 10)")]
    public AudioClip ggSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // PAKSA OBJEK INI KELUAR MENJADI ROOT AGAR TIDAK HANCUR
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            // OTOMATIS MENCARI AUDIO SOURCE (Bebas dari error salah sambung)
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                bgmSource = sources[0];
                sfxSource = sources[1];
            }

            LoadVolumeSettings();

            // Daftarkan event perpindahan scene
            SceneManager.sceneLoaded -= OnSceneLoaded; // Cegah tumpuk
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMenuSceneName) PlayBGM(bgmMainMenu);
        else PlayBGM(bgmGameplay);
    }

    private void PlayBGM(AudioClip newMusic)
    {
        // Tambahkan sistem keamanan (Mencegah MissingReferenceException)
        if (newMusic == null || bgmSource == null || bgmSource.clip == newMusic) return;

        bgmSource.Stop();
        bgmSource.clip = newMusic;
        bgmSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        if (bgmSource != null) bgmSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null) sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolumeSettings()
    {
        if (bgmSource != null) bgmSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (sfxSource != null) sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayClickSound() => PlaySFX(clickSFX);
    public void PlayMergeSound() => PlaySFX(mergeSFX);
    public void PlayGameOverSound() => PlaySFX(gameOverSFX);
    public void PlayGGSound() => PlaySFX(ggSFX);

    public void PlayComboSound(int comboTingkatIndex)
    {
        if (comboSFX.Length > 0)
        {
            int maxIndex = comboSFX.Length - 1;
            int safeIndex = Mathf.Clamp(comboTingkatIndex, 0, maxIndex);
            PlaySFX(comboSFX[safeIndex]);
        }
    }
}