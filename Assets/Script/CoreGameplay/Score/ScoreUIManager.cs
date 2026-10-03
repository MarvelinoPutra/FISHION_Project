using UnityEngine;
using System.Collections;
using TMPro; // Wajib untuk TextMeshPro

public class ScoreUIManager : MonoBehaviour
{
    [Header("Teks UI Standar")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Combo Popup (Teks)")]
    public TextMeshProUGUI comboTextPopup;
    public string[] comboMessages;

    [Header("GG GAMING Popup")]
    public TextMeshProUGUI ggGamingTextPopup;

    private void Start()
    {
        // 1. Sembunyikan pop-up di awal
        if (comboTextPopup != null) comboTextPopup.gameObject.SetActive(false);
        if (ggGamingTextPopup != null) ggGamingTextPopup.gameObject.SetActive(false);

        // 2. Berlangganan event (Subscribe)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
            ScoreManager.Instance.OnHighScoreChanged += UpdateHighScoreUI;
            ScoreManager.Instance.OnComboAchieved += ShowComboTextPopup;

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
        // 3. Berhenti berlangganan (Unsubscribe)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
            ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScoreUI;
            ScoreManager.Instance.OnComboAchieved -= ShowComboTextPopup;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMaxLevelReached -= ShowGGPopup;
        }
    }

    // --- FUNGSI MENGUBAH TEKS SKOR ---
    private void UpdateScoreUI(int score)
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }

    private void UpdateHighScoreUI(int hs)
    {
        if (highScoreText != null) highScoreText.text = "Best: " + hs.ToString();
    }

    // --- FUNGSI COMBO POPUP ---
    private void ShowComboTextPopup(int comboCount)
    {
        if (comboMessages.Length == 0 || comboTextPopup == null) return;

        int messageIndex = Mathf.Clamp(comboCount - 2, 0, comboMessages.Length - 1);
        comboTextPopup.text = comboMessages[messageIndex];

        StopAllCoroutines();
        StartCoroutine(AnimateComboPopup());
    }

    private IEnumerator AnimateComboPopup()
    {
        comboTextPopup.gameObject.SetActive(true);

        float timer = 0;
        float duration = 0.15f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0.5f, 1.2f, timer / duration);
            comboTextPopup.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        comboTextPopup.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(0.7f);
        comboTextPopup.gameObject.SetActive(false);
    }

    // --- FUNGSI GG GAMING POPUP ---
    private void ShowGGPopup()
    {
        if (ggGamingTextPopup == null) return;

        StopAllCoroutines(); // Hentikan animasi lain jika ada
        StartCoroutine(AnimateGGPopup());
    }

    private IEnumerator AnimateGGPopup()
    {
        ggGamingTextPopup.gameObject.SetActive(true);

        float timer = 0;
        float duration = 0.5f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0.1f, 1.5f, timer / duration);
            ggGamingTextPopup.transform.localScale = Vector3.one * scale;

            // Efek getar
            ggGamingTextPopup.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));
            yield return null;
        }

        ggGamingTextPopup.transform.localScale = Vector3.one * 1.2f;
        ggGamingTextPopup.transform.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(2.0f);

        ggGamingTextPopup.gameObject.SetActive(false);
    }
}