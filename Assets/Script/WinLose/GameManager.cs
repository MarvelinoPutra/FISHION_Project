using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton, biar bisa diakses dari script lain

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private int score = 0;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Menghentikan seluruh physics & Update sementara
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}