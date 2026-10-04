using UnityEngine;
using TMPro; // Untuk Teks

public class GameOverUIManager : MonoBehaviour
{
    [Header("Pengaturan Canvas")]
    public GameObject gamePlayCanvas; // Canvas HUD saat main
    public GameObject gameOverCanvas; // Canvas Pop-up Kalah

    [Header("Teks Game Over")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalHighScoreText;

    private void Start()
    {
        // Pastikan kondisi awal benar: Main nyala, Kalah mati
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);
        if (gamePlayCanvas != null) gamePlayCanvas.SetActive(true);

        // Berlangganan event Game Over dari GameManager dengan aman
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameOver += ShowGameOverScreen;
        }
    }

    private void OnDestroy()
    {
        // Berhenti berlangganan saat restart scene
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameOver -= ShowGameOverScreen;
        }
    }

    // Fungsi ini dipicu otomatis saat GameManager.Instance.TriggerGameOver() dipanggil
    private void ShowGameOverScreen()
    {
        // 1. Matikan Canvas Gameplay (Teks skor atas, tombol pause, dll menghilang)
        if (gamePlayCanvas != null) gamePlayCanvas.SetActive(false);

        // 1. Hentikan lagu BGM yang sedang berputar biar dramatis (Opsional)
        if (SoundManager.Instance != null && SoundManager.Instance.bgmSource != null)
        {
            SoundManager.Instance.bgmSource.Stop();
        }

        // 2. Putar suara Game Over!
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayGameOverSound();
        }

        // 2. Ambil data secara independen dari ScoreManager! (SRP Peak)
        // Ambil data skor dengan aman
        if (ScoreManager.Instance != null)
        {
            int scoreAkhir = ScoreManager.Instance.currentScore;

            // AMBIL HIGH SCORE LANGSUNG DARI DATABASE ANDROID (SAVE MANAGER)
            int rekorTertinggi = SaveManager.Instance != null ? SaveManager.Instance.dataDatabase.skorTertinggi : ScoreManager.Instance.highScore;

            finalScoreText.text = "Score\n" + scoreAkhir.ToString();
            finalHighScoreText.text = "High Score\n" + rekorTertinggi.ToString();
        }

        // 3. Nyalakan Canvas Game Over
        if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
    }

    // Fungsi ini tinggal di-hook ke tombol "Restart" di Inspector Unity
    public void OnRestartButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }
}