using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Pengaturan Prefab")]
    public GameObject[] spawnablePrefabs;

    private int nextFishIndex;

    void Awake()
    {
        ChooseNextFish(); // ⬅️ TAMBAHAN: tentuin ikan pertama buat preview dari awal
    }

    void ChooseNextFish() // ⬅️ TAMBAHAN: fungsi baru
    {
        if (spawnablePrefabs.Length == 0) return;
        nextFishIndex = Random.Range(0, spawnablePrefabs.Length);
         Debug.Log("Next fish dipilih: index " + nextFishIndex + " (" + spawnablePrefabs[nextFishIndex].name + ")"); // ⬅️ TAMBAHAN
    }

    public Sprite GetNextFishSprite() // ⬅️ TAMBAHAN: buat diintip DropperController
    {
        if (spawnablePrefabs.Length == 0) return null;

        SpriteRenderer sr = spawnablePrefabs[nextFishIndex].GetComponent<SpriteRenderer>();
        return sr != null ? sr.sprite : null;
    }

    public GameObject SpawnRandomFish(Vector3 spawnPosition)
    {
        if (spawnablePrefabs.Length == 0) return null;

        GameObject selectedPrefab = spawnablePrefabs[nextFishIndex];
        Debug.Log("Ikan di-spawn: " + selectedPrefab.name); // ⬅️ TAMBAHAN

        GameObject newFish = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // KUNCI SUIKA GAME: Matikan semua komponen fisika dan interaksi!
        Rigidbody2D rb = newFish.GetComponent<Rigidbody2D>();
        Collider2D col = newFish.GetComponent<Collider2D>();
        Fish fishScript = newFish.GetComponent<Fish>();

        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic; // Matikan gravitasi
        if (col != null) col.enabled = false;                    // Matikan tabrakan
        if (fishScript != null) fishScript.enabled = false;      // Matikan sistem merge

        // Setelah ikan ini muncul, langsung tentuin next-nya buat preview berikutnya
        ChooseNextFish(); // ⬅️ TAMBAHAN

        return newFish;
    }
}