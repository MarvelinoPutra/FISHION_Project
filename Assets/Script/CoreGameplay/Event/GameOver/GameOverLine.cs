using UnityEngine;
using TMPro;

public class GameOverLine : MonoBehaviour
{
    [Tooltip("Berapa detik ikan boleh nyentuh garis sebelum kalah")]
    public float timeToGameOver = 2.0f;
    private float timer = 0f;

    [Header("Efek Danger (Tarik Teks dari Canvas)")]
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        ResetDangerEffect();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance.isGameOver) return;

        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer += Time.deltaTime;
            UpdateDangerEffect();

            if (timer >= timeToGameOver)
            {
                int skorSekarang = ScoreManager.Instance != null ? ScoreManager.Instance.currentScore : 0;
                ResetDangerEffect();
                GameManager.Instance.TriggerGameOver(skorSekarang);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer = 0f;
            ResetDangerEffect();
        }
    }

    private void UpdateDangerEffect()
    {
        float sisaWaktu = timeToGameOver - timer;

        if (countdownText != null)
        {
            if (!countdownText.gameObject.activeSelf) countdownText.gameObject.SetActive(true);

            // Menggabungkan kalimat dengan angka hitung mundur
            countdownText.text = "Jangan Biarin Aquariumnya Kepenuhan!!\n" + sisaWaktu.ToString("F1");
        }
    }

    private void ResetDangerEffect()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
}