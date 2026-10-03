using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [Header("Teks UI Standar")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Combo Popup (3 Tahap: Keren -> Mantap -> GG)")]
    public Image comboImagePopup;
    [Tooltip("Isi dengan 3 Gambar: 0=Keren, 1=Mantap, 2=GG")]
    public Sprite[] comboSprites;

    [Tooltip("Isi dengan 3 Suara: 0=Keren, 1=Mantap, 2=GG")]
    public AudioClip[] comboSounds; // <--- TEMPAT MENARUH 3 SUARA COMBO

    [Header("GG GAMING Popup (Max Level 10)")]
    public Image ggGamingImagePopup;
    public Sprite[] ggSprites;

    private Vector3 initialComboScale = Vector3.one;
    private Vector3 initialGGScale = Vector3.one;

    private void Start()
    {
        // Simpan ukuran awal agar animasi tidak gepeng
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

        // Daftarkan event dari ScoreManager & GameManager
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
        // Lepas pendaftaran event saat game over / pindah scene
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

    // ========================================================
    // FUNGSI UTAMA: MENAMPILKAN COMBO 3 TAHAP (GAMBAR & SUARA)
    // ========================================================
    private void ShowComboPopup(int comboCount)
    {
        if (comboSprites.Length == 0 || comboImagePopup == null) return;

        // 1. Tentukan tingkatannya (index 0, 1, 2)
        // Kalau combo mentok ke 4 atau 5, index akan otomatis tertahan di tahap terakhir (2)
        int tingkatanIndex = Mathf.Clamp(comboCount - 2, 0, comboSprites.Length - 1);

        // 2. Ganti gambarnya (Keren -> Mantap -> GG)
        comboImagePopup.sprite = comboSprites[tingkatanIndex];

        // 3. Putar Suara yang sesuai dengan tingkatan (Keren -> Mantap -> GG)
        if (comboSounds.Length > 0 && SoundManager.Instance != null)
        {
            int soundIndex = Mathf.Clamp(comboCount - 2, 0, comboSounds.Length - 1);
            SoundManager.Instance.PlaySFX(comboSounds[soundIndex]);
        }

        // 4. Jalankan animasi membesar
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
            comboImagePopup.transform.localScale = initialComboScale * scaleMultiplier;
            yield return null;
        }

        comboImagePopup.transform.localScale = initialComboScale;
        yield return new WaitForSeconds(0.7f);
        comboImagePopup.gameObject.SetActive(false);
    }

    // ========================================================
    // FUNGSI KHUSUS LEVEL 10 (GG GAMING)
    // ========================================================
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
            ggGamingImagePopup.transform.localScale = initialGGScale * scaleMultiplier;
            ggGamingImagePopup.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));
            yield return null;
        }

        ggGamingImagePopup.transform.localScale = initialGGScale * 1.2f;
        ggGamingImagePopup.transform.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(2.0f);

        ggGamingImagePopup.gameObject.SetActive(false);
    }
}