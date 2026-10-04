using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Event broadcast saat kalah
    public Action OnGameOver;
    public bool isGameOver = false;

    public Action OnMaxLevelReached; // Event khusus untuk level 10

    public void TriggerMaxLevelReached()
    {
        OnMaxLevelReached?.Invoke();
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Dipanggil oleh garis sensor saat ikan meluap
    // 1. Versi kalau dipanggil TANPA skor (seperti dari GameOverLine)
    public void TriggerGameOver()
    {
        // Kamu bisa ganti angka 0 di bawah dengan variabel skor yang sedang aktif di gamemu (misal: currentScore)
        TriggerGameOver(0);
    }

    // 2. Versi kalau dipanggil DENGAN membawa data skor
    public void TriggerGameOver(int skorSekarang)
    {
        if (isGameOver) return;
        isGameOver = true;

        // Cek apakah skor sekarang lebih besar dari skor tertinggi di database?
        if (skorSekarang > SaveManager.Instance.dataDatabase.skorTertinggi)
        {
            // Update datanya
            SaveManager.Instance.dataDatabase.skorTertinggi = skorSekarang;

            // Simpan ke HP Android
            SaveManager.Instance.SaveData();
        }

        // Freeze waktu agar ikan berhenti bergerak & spawner berhenti
        Time.timeScale = 0f;

        // Berteriak ke semua script UI bahwa game sudah berakhir!
        OnGameOver?.Invoke();
    }

    // Dipanggil oleh tombol Restart di UI
    public void RestartGame()
    {
        // Putar suara klik
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1f; // Kembalikan waktu agar tidak beku
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ==========================================
    // FUNGSI BARU: KEMBALI KE MAIN MENU
    // ==========================================
    public void ReturnToMainMenu()
    {
        // Putar suara klik
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();
        Time.timeScale = 1f; // Wajib dikembalikan ke 1 agar Main Menu tidak ikut beku
        SceneManager.LoadScene("MainMenu");
    }
}