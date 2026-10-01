using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    [Header("Pengaturan Game Over")]
    public float timeToGameOver = 2f; // Berapa lama ikan harus nyangkut sebelum Game Over?

    private float timer = 0f;

    // Pastikan objek Garis Game Over memiliki BoxCollider2D dengan 'Is Trigger' = TRUE (dicentang)
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh garis adalah objek Ikan
        Fish fish = collision.GetComponent<Fish>();
        if (fish != null)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // CEK LOGIKA FISIKA: 
            // rb.velocity.y mengecek kecepatan naik-turun. 
            // Kalau mendekati 0, artinya ikan sedang DIAM/NYANGKUT, bukan sedang jatuh.
            if (rb != null && Mathf.Abs(rb.velocity.y) < 0.1f)
            {
                // Ikan nyangkut! Mulai hitung mundur
                timer += Time.deltaTime;

                if (timer >= timeToGameOver)
                {
                    TriggerGameOver();
                }
            }
            else
            {
                // Jika ikan masih bergerak (sedang jatuh meluncur ke bawah), JANGAN dihitung!
                timer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kalau ikannya berhasil jatuh melewati garis atau turun ke bawah, reset waktu aman.
        Fish fish = collision.GetComponent<Fish>();
        if (fish != null)
        {
            timer = 0f;
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("GAME OVER! Tumpukan Ikan Terlalu Tinggi!");

        // --- Panggil sistem Game Over milikmu di bawah sini ---
        // GameManager.Instance.GameOver();

        // Mematikan script ini agar tidak memanggil Debug.Log berkali-kali
        this.enabled = false;
    }
}