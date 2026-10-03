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
    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Freeze waktu agar ikan berhenti bergerak & spawner berhenti
        Time.timeScale = 0f;

        // Berteriak ke semua script UI bahwa game sudah berakhir!
        OnGameOver?.Invoke();
    }

    // Dipanggil oleh tombol Restart di UI
    public void RestartGame()
    {
        Time.timeScale = 1f; // Kembalikan waktu agar tidak beku
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ==========================================
    // FUNGSI BARU: KEMBALI KE MAIN MENU
    // ==========================================
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Wajib dikembalikan ke 1 agar Main Menu tidak ikut beku
        SceneManager.LoadScene("MainMenu");
    }
}