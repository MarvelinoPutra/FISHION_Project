using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Event broadcast saat kalah
    public Action OnGameOver;
    public bool isGameOver = false;

    public Action OnMaxLevelReached;

    [Header("UI Tutorial")]
    public GameObject tutorialPanel; // Tarik UI Tutorialmu ke sini di Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // ==========================================
        // CEK TUTORIAL SAAT MAIN GAME DIMULAI
        // ==========================================
        if (SaveManager.Instance != null && SaveManager.Instance.dataDatabase != null)
        {
            if (SaveManager.Instance.dataDatabase.sudahTutorial == false)
            {
                // Belum pernah main -> Munculkan tutorial, freeze waktu
                if (tutorialPanel != null) tutorialPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                // Sudah pernah main -> Sembunyikan tutorial
                if (tutorialPanel != null) tutorialPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL "OK" DI TUTORIAL
    // ==========================================
    public void TutupTutorial()
    {
        // 1. Simpan ingatan ke JSON Android
        if (SaveManager.Instance != null && SaveManager.Instance.dataDatabase != null)
        {
            SaveManager.Instance.dataDatabase.sudahTutorial = true;
            SaveManager.Instance.SaveData();
        }

        // 2. Sembunyikan UI dan jalankan gamenya
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();

        Time.timeScale = 1f; // Jalankan waktu kembali
    }

    public void TriggerMaxLevelReached()
    {
        OnMaxLevelReached?.Invoke();
    }

    public void TriggerGameOver()
    {
        TriggerGameOver(0);
    }

    public void TriggerGameOver(int skorSekarang)
    {
        if (isGameOver) return;
        isGameOver = true;

        if (SaveManager.Instance != null && SaveManager.Instance.dataDatabase != null)
        {
            if (skorSekarang > SaveManager.Instance.dataDatabase.skorTertinggi)
            {
                SaveManager.Instance.dataDatabase.skorTertinggi = skorSekarang;
                SaveManager.Instance.SaveData();
            }
        }

        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene("MainMenu");
    }
}