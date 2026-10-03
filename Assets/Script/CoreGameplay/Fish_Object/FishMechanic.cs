using UnityEngine;

public class Fish : MonoBehaviour
{
    public int level;                  // Level ikan ini (0 = paling kecil)
    public GameObject nextLevelPrefab; // Prefab ikan hasil merge (level + 1)

    [Header("Visual Effects (VFX)")]
    public GameObject bubblePopEffectPrefab; // Drag prefab BubblePopParticle di sini

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. GUARD CLAUSE: Jangan lakukan apa-apa kalau game sudah over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        // 2. Ambil komponen FishMechanic dari objek yang ditabrak
        Fish otherFish = collision.gameObject.GetComponent<Fish>();

        // 3. Kalau objek yang ditabrak juga ikan, dan levelnya SAMA
        if (otherFish != null && otherFish.level == this.level)
        {
            Merge(otherFish);
        }
    }

    private void Merge(Fish other)
    {
        // Mencegah dua ikan memicu Merge secara bersamaan (mencegah bug spawn ganda)
        if (this.GetInstanceID() > other.GetInstanceID()) return;

        Vector2 mergePos = (transform.position + other.transform.position) / 2f;

        // ==========================================
        // MUNCULKAN EFEK PARTIKEL BUBBLE POP
        // ==========================================
        if (bubblePopEffectPrefab != null)
        {
            Instantiate(bubblePopEffectPrefab, mergePos, Quaternion.identity);
        }

        // ==========================================
        // 2. MEMAINKAN SUARA FUSE (BUBBLE)
        // ==========================================
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMergeSound(); // <--- TAMBAHKAN BARIS INI
        }

        // ==========================================
        // LOGIKA KHUSUS LEVEL MAKSIMAL (LEVEL 10)
        // ==========================================
        if (this.level >= 10)
        {
            // Lapor ke Wasit untuk memunculkan teks "GG GAMING!"
            if (GameManager.Instance != null) GameManager.Instance.TriggerMaxLevelReached();

            // Tambah skor terakhir
            if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(this.level);

            // Hancurkan ikan tanpa memunculkan ikan baru
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            return; // Hentikan fungsi di sini
        }

        // ==========================================
        // LOGIKA NORMAL (LEVEL 0 SAMPAI 9)
        // ==========================================
        if (nextLevelPrefab != null)
        {
            // Munculkan ikan level selanjutnya
            GameObject newFish = Instantiate(nextLevelPrefab, mergePos, Quaternion.identity);
            newFish.name = nextLevelPrefab.name; // Opsional: hilangkan tulisan (Clone)
        }

        // Hancurkan kedua ikan lama
        Destroy(other.gameObject);
        Destroy(this.gameObject);

        // Tambah skor menggunakan sistem ScoreManager (SRP)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(this.level);
        }
    }
}