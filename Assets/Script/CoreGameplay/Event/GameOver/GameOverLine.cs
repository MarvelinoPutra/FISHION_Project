using UnityEngine;
using TMPro;

public class GameOverLine : MonoBehaviour
{
    [Header("Pengaturan Waktu (Detik)")]
    [Tooltip("Waktu ikan boleh numpang lewat jatuh tanpa memicu teks peringatan")]
    public float delaySebelumTeks = 2.0f;

    [Tooltip("Lama waktu hitung mundur setelah teks muncul sebelum Game Over")]
    public float waktuHitungMundur = 3.0f;

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

        // Memastikan yang menyentuh garis adalah objek fisik (ikan)
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer += Time.deltaTime;

            // Jika ikan sudah berada di area garis LEBIH LAMA dari waktu delay (nyangkut/numpuk)
            if (timer >= delaySebelumTeks)
            {
                UpdateDangerEffect();

                // Jika waktu nyangkut sudah melebihi total waktu (delay + hitung mundur)
                if (timer >= (delaySebelumTeks + waktuHitungMundur))
                {
                    int skorSekarang = ScoreManager.Instance != null ? ScoreManager.Instance.currentScore : 0;
                    ResetDangerEffect();
                    GameManager.Instance.TriggerGameOver(skorSekarang);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Kalau ikan yang jatuh berhasil turun melewati garis, reset timer-nya
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer = 0f;
            ResetDangerEffect();
        }
    }

    private void UpdateDangerEffect()
    {
        // Menghitung sisa detik hitung mundur (mengabaikan waktu delay awal)
        float waktuPeringatanBerjalan = timer - delaySebelumTeks;
        float sisaWaktu = waktuHitungMundur - waktuPeringatanBerjalan;

        if (countdownText != null)
        {
            // Nyalakan teks hanya saat fungsi ini dipanggil
            if (!countdownText.gameObject.activeSelf) countdownText.gameObject.SetActive(true);

            // Mencegah angkanya menampilkan nilai minus (-0.1) saat transisi Game Over
            if (sisaWaktu < 0) sisaWaktu = 0;

            countdownText.text = "Jangan Biarin Aquariumnya Kepenuhan!!\n" + sisaWaktu.ToString("F1");
        }
    }

    private void ResetDangerEffect()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
}