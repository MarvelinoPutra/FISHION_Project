using UnityEngine;

// Wajib ditambahkan agar Unity bisa mengubah data ini menjadi file JSON
[System.Serializable]
public class GameData
{
    // ==========================================
    // ISI DENGAN DATA YANG MAU DISIMPAN DI SINI
    // ==========================================

    public int skorTertinggi; // High Score
    public bool tutorialSelesai; // Apakah pemain sudah pernah lihat tutorial?

    // (Bisa tambah pengaturan volume kalau mau, dll)
    // public float musicVolume;

    // Nilai Default saat pertama kali game diinstal
    public GameData()
    {
        skorTertinggi = 0;
        tutorialSelesai = false;
    }
}