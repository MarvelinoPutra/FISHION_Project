using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Pengaturan Prefab")]
    public GameObject[] spawnablePrefabs;

    public GameObject SpawnRandomFish(Vector3 spawnPosition)
    {
        if (spawnablePrefabs.Length == 0) return null;

        int randomIndex = Random.Range(0, spawnablePrefabs.Length);
        GameObject selectedPrefab = spawnablePrefabs[randomIndex];

        GameObject newFish = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // KUNCI SUIKA GAME: Matikan semua komponen fisika dan interaksi!
        Rigidbody2D rb = newFish.GetComponent<Rigidbody2D>();
        Collider2D col = newFish.GetComponent<Collider2D>();
        Fish fishScript = newFish.GetComponent<Fish>();

        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic; // Matikan gravitasi
        if (col != null) col.enabled = false;                    // Matikan tabrakan
        if (fishScript != null) fishScript.enabled = false;      // Matikan sistem merge

        return newFish;
    }
}