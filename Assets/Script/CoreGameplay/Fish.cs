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
        // (karena OnCollisionEnter2D terpanggil di kedua objek)
        if (this.GetInstanceID() > other.GetInstanceID()) return;

        Vector2 mergePos = (transform.position + other.transform.position) / 2f;

        if (nextLevelPrefab != null)
        {
            Instantiate(nextLevelPrefab, mergePos, Quaternion.identity);
        }

        // Tambah skor lewat GameManager (dibahas di bagian 7)
        GameManager.Instance.AddScore(level + 1);

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}