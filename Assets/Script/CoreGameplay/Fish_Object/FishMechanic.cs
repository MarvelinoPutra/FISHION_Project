using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int level;                  // Level ikan ini (0 = paling kecil)
    public GameObject nextLevelPrefab; // Prefab ikan hasil merge (level + 1)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fish otherFish = collision.gameObject.GetComponent<Fish>();

        // Kalau objek yang ditabrak juga ikan, dan levelnya SAMA
        if (otherFish != null && otherFish.level == this.level)
        {
            Merge(otherFish);
        }
        // Kalau levelnya BEDA, tidak ada aksi apa-apa di sini,
        // sehingga yang terjadi hanya benturan fisika biasa (memantul),
        // efek pantulnya diatur otomatis oleh Physics Material 2D
    }

    void Merge(Fish other)
    {
        // Mencegah kedua ikan sama-sama memicu Merge() secara bersamaan
        if (this.GetInstanceID() > other.GetInstanceID()) return;

        Vector2 mergePos = (transform.position + other.transform.position) / 2f;

        // 1. Munculkan ikan baru
        if (nextLevelPrefab != null)
        {
            GameObject newFish = Instantiate(nextLevelPrefab, mergePos, Quaternion.identity);
            newFish.name = nextLevelPrefab.name; // Opsional: menghilangkan tulisan (Clone)
        }

        // 2. HANCURKAN IKAN LAMA TERLEBIH DAHULU (Supaya dijamin pasti hilang)
        Destroy(other.gameObject);
        Destroy(this.gameObject);

        // 3. Tambah skor (Diamankan dengan pengecekan agar tidak bikin crash)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(level + 1);
        }
        else
        {
            Debug.LogWarning("Peringatan: GameManager belum ada di Scene, tapi merge berhasil!");
        }
    }
}