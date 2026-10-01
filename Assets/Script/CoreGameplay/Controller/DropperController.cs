using System.Collections;
using UnityEngine;

// Memastikan script Input dan Spawner harus ada di objek yang sama
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(FishSpawner))]
public class DropperController : MonoBehaviour
{
    [Header("Pengaturan Area & Waktu")]
    public Transform spawnPoint;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float nextSpawnDelay = 1f;

    // Referensi ke dua script lainnya
    private PlayerInput playerInput;
    private FishSpawner fishSpawner;

    private GameObject currentFish;
    private bool isWaiting = false;

    void Awake()
    {
        // Ambil komponen secara otomatis
        playerInput = GetComponent<PlayerInput>();
        fishSpawner = GetComponent<FishSpawner>();
    }

    void Start()
    {
        PrepareNewFish();
    }

    void Update()
    {
        if (currentFish == null || isWaiting) return;

        // Baca dari PlayerInput
        if (playerInput.HasValidInput)
        {
            MoveFish(playerInput.InputWorldPosition);
        }

        if (playerInput.IsDropping)
        {
            DropFish();
        }
    }

    void MoveFish(Vector3 targetWorldPosition)
    {
        float clampedX = Mathf.Clamp(targetWorldPosition.x, minX, maxX);
        currentFish.transform.position = new Vector3(clampedX, spawnPoint.position.y, 0f);
    }

    void DropFish()
    {
        // Pastikan pas dilepas, fisikanya benar-benar dinyalakan KE IKAN YANG INI DOANG
        Rigidbody2D rb = currentFish.GetComponent<Rigidbody2D>();
        Collider2D col = currentFish.GetComponent<Collider2D>();
        Fish fishScript = currentFish.GetComponent<Fish>();

        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;

        // NYALAKAN KEMBALI DENGAN TEGAS DI SINI
        if (col != null) col.enabled = true;
        if (fishScript != null) fishScript.enabled = true;

        currentFish = null;
        StartCoroutine(WaitAndSpawnNext());
    }

    IEnumerator WaitAndSpawnNext()
    {
        isWaiting = true;
        yield return new WaitForSeconds(nextSpawnDelay);
        PrepareNewFish();
        isWaiting = false;
    }

    void PrepareNewFish()
    {
        // Minta ikan baru dari FishSpawner
        currentFish = fishSpawner.SpawnRandomFish(spawnPoint.position);
    }
}