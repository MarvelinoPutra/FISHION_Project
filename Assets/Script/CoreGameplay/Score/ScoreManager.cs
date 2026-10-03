using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Informasi Skor (Hanya Baca)")]
    public int currentScore;
    public int highScore;
    public int currentCombo;

    [Header("Pengaturan Combo")]
    public float comboTimeout = 1.5f; // Waktu maksimal antar merge untuk dihitung combo
    private float lastMergeTime;

    // C# Events (Broadcast System) untuk memberi tahu UI
    public Action<int> OnScoreChanged;
    public Action<int> OnHighScoreChanged;
    public Action<int> OnComboAchieved;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Load High Score yang tersimpan di HP
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        // Beri tahu UI nilai awal saat game mulai
        OnHighScoreChanged?.Invoke(highScore);
        OnScoreChanged?.Invoke(currentScore);
    }

    // Dipanggil oleh ikan saat nge-fuse
    public void AddScore(int fishLevel)
    {
        // Rumus: Level 0 = 5, Level 1 = 10, Level 2 = 15, dst.
        int point = (fishLevel + 1) * 5;
        currentScore += point;
        OnScoreChanged?.Invoke(currentScore);

        // Logika Combo berantai
        if (Time.time - lastMergeTime <= comboTimeout)
        {
            currentCombo++;
            if (currentCombo > 1)
            {
                OnComboAchieved?.Invoke(currentCombo); // Panggil Popup Combo!
            }
        }
        else
        {
            currentCombo = 1; // Reset combo karena kelamaan
        }
        lastMergeTime = Time.time;

        // Logika High Score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            OnHighScoreChanged?.Invoke(highScore);
        }
    }
}