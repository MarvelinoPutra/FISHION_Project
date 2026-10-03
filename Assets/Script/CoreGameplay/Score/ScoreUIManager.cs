using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [Header("Teks UI Standar")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Combo Popup (Gambar)")]
    public Image comboImagePopup;
    public Sprite[] comboSprites;

    [Header("GG GAMING Popup (Gambar Acak)")]
    public Image ggGamingImagePopup;
    public Sprite[] ggSprites;

    // --- VARIABEL BARU UNTUK MENGINGAT UKURAN ASLI DI EDITOR ---
    private Vector3 initialComboScale = Vector3.one;
    private Vector3 initialGGScale = Vector3.one;

    private void Start()
    {
        // Simpan ukuran aslinya sebelum disembunyikan
        if (comboImagePopup != null)
        {
            initialComboScale = comboImagePopup.transform.localScale;
            comboImagePopup.gameObject.SetActive(false);
        }

        if (ggGamingImagePopup != null)
        {
            initialGGScale = ggGamingImagePopup.transform.localScale;
            ggGamingImagePopup.gameObject.SetActive(false);
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
            ScoreManager.Instance.OnHighScoreChanged += UpdateHighScoreUI;
            ScoreManager.Instance.OnComboAchieved += ShowComboPopup;

            UpdateScoreUI(ScoreManager.Instance.currentScore);
            UpdateHighScoreUI(ScoreManager.Instance.highScore);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMaxLevelReached += ShowGGPopup;
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
            ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScoreUI;
            ScoreManager.Instance.OnComboAchieved -= ShowComboPopup;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMaxLevelReached -= ShowGGPopup;
        }
    }

    private void UpdateScoreUI(int score)
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }

    private void UpdateHighScoreUI(int hs)
    {
        if (highScoreText != null) highScoreText.text = "Best: " + hs.ToString();
    }

    private void ShowComboPopup(int comboCount)
    {
        if (comboSprites.Length == 0 || comboImagePopup == null) return;

        int spriteIndex = Mathf.Clamp(comboCount - 2, 0, comboSprites.Length - 1);
        comboImagePopup.sprite = comboSprites[spriteIndex];

        StopAllCoroutines();
        StartCoroutine(AnimateComboPopup());
    }

    private IEnumerator AnimateComboPopup()
    {
        comboImagePopup.gameObject.SetActive(true);

        float timer = 0;
        float duration = 0.15f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scaleMultiplier = Mathf.Lerp(0.5f, 1.2f, timer / duration);

            // KUNCI PERBAIKAN: Kalikan animasi dengan ukuran asli dari Editor
            comboImagePopup.transform.localScale = initialComboScale * scaleMultiplier;
            yield return null;
        }

        // Kembalikan ke ukuran asli
        comboImagePopup.transform.localScale = initialComboScale;
        yield return new WaitForSeconds(0.7f);
        comboImagePopup.gameObject.SetActive(false);
    }

    private void ShowGGPopup()
    {
        if (ggGamingImagePopup == null) return;

        if (ggSprites != null && ggSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, ggSprites.Length);
            ggGamingImagePopup.sprite = ggSprites[randomIndex];
        }

        StopAllCoroutines();
        StartCoroutine(AnimateGGPopup());
    }

    private IEnumerator AnimateGGPopup()
    {
        ggGamingImagePopup.gameObject.SetActive(true);

        float timer = 0;
        float duration = 0.5f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scaleMultiplier = Mathf.Lerp(0.1f, 1.5f, timer / duration);

            // KUNCI PERBAIKAN: Kalikan animasi dengan ukuran asli dari Editor
            ggGamingImagePopup.transform.localScale = initialGGScale * scaleMultiplier;

            ggGamingImagePopup.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));
            yield return null;
        }

        // Ukuran normal akhir adalah 1.2 kali lipat dari ukuran aslinya
        ggGamingImagePopup.transform.localScale = initialGGScale * 1.2f;
        ggGamingImagePopup.transform.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(2.0f);

        ggGamingImagePopup.gameObject.SetActive(false);
    }
}