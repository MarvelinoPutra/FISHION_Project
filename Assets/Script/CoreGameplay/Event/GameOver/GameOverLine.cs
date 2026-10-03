using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    [Tooltip("Berapa detik ikan boleh nyentuh garis sebelum kalah")]
    public float timeToGameOver = 2.0f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Kalau sudah game over, abaikan.
        if (GameManager.Instance.isGameOver) return;

        // Cek apakah yang menyentuh garis adalah Ikan (punya Rigidbody2D)
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer += Time.deltaTime; // Mulai hitung mundur

            if (timer >= timeToGameOver)
            {
                // Lapor ke Wasit!
                GameManager.Instance.TriggerGameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Kalau ikan turun lagi (masuk air), reset timernya
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            timer = 0f;
        }
    }
}